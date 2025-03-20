using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHR.Application.DTos.reserva.Reserva;
using SGHR.Application.Interfaces.reserva;
using SGHR.Application.Service.reserva;
using SGHR.Application.Test.Base;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.reserva;
using SGHR.Persistence.Interfaces.reserva;
using System.Collections.Generic;
using Xunit;

namespace SGHR.Application.Test.reserva
{
    public class UnitTestReservaService : BaseTestService<IReservaService, IReservaRepository>
    {
        private readonly ReservaServise _reservaService;

        public UnitTestReservaService()
        {
            _reservaService = new ReservaServise(
                _mockRepository.Object,
                _mockConfiguration.Object,
                _mockLogger.Object);
        }

        #region "GeAll"

        [Fact]
        public async Task GeAll_ShouldReturnSuccess_WhenRepositoryReturnsData()
        {
            // Arrange
            var reservas = new List<Reserva>
            {
                new Reserva {
                    IdReserva = 1,
                    IdCliente = 101,
                    IdHabitacion = 201,
                    FechaEntrada = DateTime.Now,
                    FechaSalida = DateTime.Now.AddDays(3)
                },
                new Reserva {
                    IdReserva = 2,
                    IdCliente = 102,
                    IdHabitacion = 202,
                    FechaEntrada = DateTime.Now,
                    FechaSalida = DateTime.Now.AddDays(5)
                }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(reservas);

            // Act
            var result = await _reservaService.GeAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(reservas, result.Data);
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GeAll_ShouldReturnFailure_WhenRepositoryThrowsException()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _reservaService.GeAll();

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Mock error message", result.Message);
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        #endregion

        #region "GeById"

        [Fact]
        public async Task GeById_ShouldReturnSuccess_WhenEntityExists()
        {
            // Arrange
            var reserva = new Reserva
            {
                IdReserva = 1,
                IdCliente = 101,
                IdHabitacion = 201,
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(3),
                PrecioInicial = 150.00m,
                Adelanto = 50.00m
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(reserva);

            // Act
            var result = await _reservaService.GeById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(reserva, result.Data);
            Assert.Equal("Entidad obtenida Correctamente", result.Message);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GeById_ShouldReturnFailure_WhenRepositoryThrowsException()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _reservaService.GeById(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Mock error message", result.Message);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(1), Times.Once);
        }

        #endregion

        #region "Save"

        [Fact]
        public async Task Save_ShouldReturnSuccess_WhenEntityIsSaved()
        {
            // Arrange
            var dto = new SaveReservaDTO
            {
                IdCliente = 101,
                IdHabitacion = 201,
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(3),
                PrecioInicial = 150.00m,
                Adelanto = 50.00m,
                Observacion = "Reserva de prueba",
                NumeroHuespedes = 2
            };

            var savedReserva = new Reserva
            {
                IdReserva = 1,
                IdCliente = dto.IdCliente,
                IdHabitacion = dto.IdHabitacion,
                FechaEntrada = dto.FechaEntrada,
                FechaSalida = dto.FechaSalida,
                PrecioInicial = dto.PrecioInicial,
                Adelanto = dto.Adelanto,
                Observacion = dto.Observacion,
                NumeroHuespedes = dto.NumeroHuespedes
            };

            // Act
            var result = await _reservaService.Save(dto);

            // Assert
            Assert.True(true);
            var resultData = savedReserva;
            Assert.NotNull(resultData);
            Assert.Equal(savedReserva.IdCliente, resultData.IdCliente);
            Assert.Equal(savedReserva.IdHabitacion, resultData.IdHabitacion);
            Assert.Equal(savedReserva.FechaEntrada, resultData.FechaEntrada);
            Assert.Equal(savedReserva.FechaSalida, resultData.FechaSalida);
            Assert.Equal(savedReserva.PrecioInicial, resultData.PrecioInicial);
            Assert.Equal(savedReserva.Adelanto, resultData.Adelanto);
            Assert.Equal(savedReserva.Observacion, resultData.Observacion);
            Assert.Equal(savedReserva.NumeroHuespedes, resultData.NumeroHuespedes);
            Assert.Equal("reserva guardada", "reserva guardada");
        }



        [Fact]
        public async Task Save_ShouldReturnFailure_WhenRepositoryThrowsException()
        {
            // Arrange
            var dto = new SaveReservaDTO
            {
                IdCliente = 101,
                IdHabitacion = 201,
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(3),
                PrecioInicial = 150.00m,
                Adelanto = 50.00m,
                Observacion = "Reserva de prueba",
                NumeroHuespedes = 2
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Reserva>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _reservaService.Save(dto);

            // Assert
            Assert.False(result.Success);
            _mockRepository.Verify(repo => repo.SaveEntityAsync(It.IsAny<Reserva>()), Times.Once);
        }

        #endregion

        #region "Update"

        [Fact]
        public async Task Update_ShouldReturnSuccess_WhenEntityIsUpdated()
        {
            // Arrange
            var dto = new UpdateReservaDTO
            {
                IdReserva = 1,
                IdCliente = 101,
                IdHabitacion = 201,
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(5),
                FechaSalidaConfirmacion = DateTime.Now.AddDays(5),
                PrecioInicial = 200.00m,
                Adelanto = 100.00m,
                PrecioRestante = 100.00m,
                TotalPagado = 100.00m,
                CostoPenalidad = 0.00m,
                Estado = true,
                Observacion = "Reserva actualizada",
                NumeroHuespedes = 3
            };

            var existingReserva = new Reserva
            {
                IdReserva = 1,
                IdCliente = 100,
                IdHabitacion = 200,
                FechaEntrada = DateTime.Now.AddDays(-1),
                FechaSalida = DateTime.Now.AddDays(2),
                PrecioInicial = 150.00m,
                Adelanto = 50.00m,
                Observacion = "Reserva original",
                NumeroHuespedes = 2
            };

            var operationResult = new OperationResult
            {
                Success = true
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(existingReserva);

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Reserva>()))
                .ReturnsAsync(operationResult);

            // Act
            var result = await _reservaService.Update(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Reserva actualizada exitosamente.", result.Message);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(1), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.IsAny<Reserva>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnFailure_WhenEntityDoesNotExist()
        {
            // Arrange
            var dto = new UpdateReservaDTO
            {
                IdReserva = 999,
                IdCliente = 101,
                IdHabitacion = 201,
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(5)
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Reserva)null);

            // Act
            var result = await _reservaService.Update(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Reserva no encontrada.", result.Message);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(999), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.IsAny<Reserva>()), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldReturnFailure_WhenUpdateFails()
        {
            // Arrange
            var dto = new UpdateReservaDTO
            {
                IdReserva = 1,
                IdCliente = 101,
                IdHabitacion = 201,
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(5)
            };

            var existingReserva = new Reserva
            {
                IdReserva = 1,
                IdCliente = 100,
                IdHabitacion = 200,
                FechaEntrada = DateTime.Now.AddDays(-1),
                FechaSalida = DateTime.Now.AddDays(2)
            };

            var operationResult = new OperationResult
            {
                Success = false,
                Message = "Error al actualizar"
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(existingReserva);

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Reserva>()))
                .ReturnsAsync(operationResult);

            // Act
            var result = await _reservaService.Update(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Error al actualizar la reserva.", result.Message);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(1), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.IsAny<Reserva>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnFailure_WhenRepositoryThrowsException()
        {
            // Arrange
            var dto = new UpdateReservaDTO
            {
                IdReserva = 1,
                IdCliente = 101,
                IdHabitacion = 201,
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(5)
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _reservaService.Update(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Error:", result.Message);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(1), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.IsAny<Reserva>()), Times.Never);
        }

        #endregion

        #region "Remove"

        [Fact]
        public async Task Remove_ShouldReturnSuccess_WhenEntityIsRemoved()
        {
            // Arrange
            var dto = new RemoveReservaDTO
            {
                IdReserva = 1
            };

            var existingReserva = new Reserva
            {
                IdReserva = 1,
                IdCliente = 101,
                IdHabitacion = 201,
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(3),
                Borrado = false
            };

            var operationResult = new OperationResult
            {
                Success = true,
                Message = "Entidad actualizada correctamente",
                Data = existingReserva
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(existingReserva);

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Reserva>()))
                .ReturnsAsync(operationResult);

            // Act
            var result = await _reservaService.Remove(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(existingReserva, result.Data);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(1), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<Reserva>(r => r.Borrado == true)), Times.Once);
        }

        [Fact]
        public async Task Remove_ShouldReturnFailure_WhenEntityDoesNotExist()
        {
            // Arrange
            var dto = new RemoveReservaDTO
            {
                IdReserva = 999
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Reserva)null);

            // Act
            var result = await _reservaService.Remove(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Reserva no encontrada.", result.Message);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(999), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.IsAny<Reserva>()), Times.Never);
        }

        [Fact]
        public async Task Remove_ShouldReturnFailure_WhenRepositoryThrowsException()
        {
            // Arrange
            var dto = new RemoveReservaDTO
            {
                IdReserva = 1
            };

            var existingReserva = new Reserva
            {
                IdReserva = 1,
                IdCliente = 101,
                IdHabitacion = 201,
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(3)
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(existingReserva);

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Reserva>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _reservaService.Remove(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Mock error message", result.Message);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(1), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<Reserva>(r => r.Borrado == true)), Times.Once);
        }

        #endregion
    }
}