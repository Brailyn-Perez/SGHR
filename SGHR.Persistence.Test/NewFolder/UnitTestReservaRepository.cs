using SGHR.Domain.Entities.reserva;
using SGHR.Persistence.Repositories.reserva;
using SGHR.Persistence.Test.habitacion.Base;
using System;
using System.Linq.Expressions;
using SGHR.Domain.Base;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SGHR.Persistence.Test.NewFolder
{
    public class UnitTestReservaRepository : BaseTest<ReservaRepository>
    {
        private readonly ReservaRepository _reservaRepository;

        public UnitTestReservaRepository()
        {
            _reservaRepository = new ReservaRepository(_context, _mockLogger.Object, _mockConfiguration.Object);
        }

        #region "GetEntityByIdAsync"
        [Fact]
        public async Task GetEntityByIdAsync_ShouldThrowArgumentException_WhenIdIsZeroOrNegative()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _reservaRepository.GetEntityByIdAsync(0));
            await Assert.ThrowsAsync<ArgumentException>(() => _reservaRepository.GetEntityByIdAsync(-1));
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldThrowKeyNotFoundException_WhenEntityNotFound()
        {
            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _reservaRepository.GetEntityByIdAsync(999));
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnReserva_WhenEntityExists()
        {
            // Arrange
            var reserva = CreateSampleReserva();
            var result = await _reservaRepository.SaveEntityAsync(reserva);
            var savedReserva = (Reserva)result.Data;

            // Act
            var retrievedReserva = await _reservaRepository.GetEntityByIdAsync(savedReserva.IdReserva);

            // Assert
            Assert.NotNull(retrievedReserva);
            Assert.Equal(savedReserva.IdReserva, retrievedReserva.IdReserva);
        }
        #endregion

        #region "GetAllAsync"
        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoReservas()
        {
            // Act
            var result = await _reservaRepository.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnReservas_WhenReservasExist()
        {
            // Arrange
            var reserva = CreateSampleReserva();
            await _reservaRepository.SaveEntityAsync(reserva);

            // Act
            var result = await _reservaRepository.GetAllAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldNotReturnDeletedReservas()
        {
            // Arrange
            var reserva = CreateSampleReserva();
            var saveResult = await _reservaRepository.SaveEntityAsync(reserva);
            var savedReserva = (Reserva)saveResult.Data;

            // Mark as deleted
            savedReserva.Borrado = true;
            savedReserva.UsuarioActualizacion = 1;
            await _reservaRepository.UpdateEntityAsync(savedReserva);

            // Act
            var result = await _reservaRepository.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }
        #endregion

        #region "SaveEntityAsync"
        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenValidEntityProvided()
        {
            // Arrange
            var reserva = CreateSampleReserva();

            // Act
            var result = await _reservaRepository.SaveEntityAsync(reserva);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Contains("exitosamente", result.Message);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldFail_WhenNullEntityProvided()
        {
            // Act
            var result = await _reservaRepository.SaveEntityAsync(null);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("no puede ser nula", result.Message);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldFail_WhenInvalidClienteId()
        {
            // Arrange
            var reserva = CreateSampleReserva();
            reserva.IdCliente = 0;

            // Act
            var result = await _reservaRepository.SaveEntityAsync(reserva);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("cliente", result.Message.ToLower());
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldFail_WhenInvalidHabitacionId()
        {
            // Arrange
            var reserva = CreateSampleReserva();
            reserva.IdHabitacion = 0;

            // Act
            var result = await _reservaRepository.SaveEntityAsync(reserva);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("habitacion", result.Message.ToLower());
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldFail_WhenEntryDateAfterExitDate()
        {
            // Arrange
            var reserva = CreateSampleReserva();
            reserva.FechaEntrada = DateTime.Now.AddDays(5);
            reserva.FechaSalida = DateTime.Now.AddDays(2);

            // Act
            var result = await _reservaRepository.SaveEntityAsync(reserva);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("fecha", result.Message.ToLower());
        }
        #endregion

        #region "UpdateEntityAsync"
        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenValidEntityProvided()
        {
            // Arrange
            var reserva = CreateSampleReserva();
            var saveResult = await _reservaRepository.SaveEntityAsync(reserva);
            var savedReserva = (Reserva)saveResult.Data;

            savedReserva.NumeroHuespedes = 4;
            savedReserva.Observacion = "Actualizada";
            savedReserva.UsuarioActualizacion = 1;

            // Act
            var result = await _reservaRepository.UpdateEntityAsync(savedReserva);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            var updatedReserva = (Reserva)result.Data;
            Assert.Equal(4, updatedReserva.NumeroHuespedes);
            Assert.Equal("Actualizada", updatedReserva.Observacion);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldFail_WhenNullEntityProvided()
        {
            // Act
            var result = await _reservaRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("no puede ser nula", result.Message);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldFail_WhenNoUsuarioActualizacion()
        {
            // Arrange
            var reserva = CreateSampleReserva();
            var saveResult = await _reservaRepository.SaveEntityAsync(reserva);
            var savedReserva = (Reserva)saveResult.Data;

            savedReserva.Observacion = "Actualizada";

            // Act
            var result = await _reservaRepository.UpdateEntityAsync(savedReserva);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("usuario", result.Message.ToLower());
        }
        #endregion

        #region "ExistsAsync"
        [Fact]
        public async Task ExistsAsync_ShouldThrowArgumentNullException_WhenFilterIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _reservaRepository.ExistsAsync(null));
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnFalse_WhenNoMatchingReserva()
        {
            // Arrange
            Expression<Func<Reserva, bool>> filter = r => r.IdCliente == 999;

            // Act
            var result = await _reservaRepository.ExistsAsync(filter);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnTrue_WhenMatchingReservaExists()
        {
            // Arrange
            var reserva = CreateSampleReserva();
            await _reservaRepository.SaveEntityAsync(reserva);

            Expression<Func<Reserva, bool>> filter = r => r.IdCliente == reserva.IdCliente;

            // Act
            var result = await _reservaRepository.ExistsAsync(filter);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ExistsAsync_ShouldNotFindDeletedReservas()
        {
            // Arrange
            var reserva = CreateSampleReserva();
            var saveResult = await _reservaRepository.SaveEntityAsync(reserva);
            var savedReserva = (Reserva)saveResult.Data;

            savedReserva.Borrado = true;
            savedReserva.UsuarioActualizacion = 1;
            await _reservaRepository.UpdateEntityAsync(savedReserva);

            Expression<Func<Reserva, bool>> filter = r => r.IdCliente == reserva.IdCliente;

            // Act
            var result = await _reservaRepository.ExistsAsync(filter);

            // Assert
            Assert.False(result);
        }
        #endregion

        #region "GetAllAsync with Filter"
        [Fact]
        public async Task GetAllAsyncWithFilter_ShouldReturnSuccess_WhenFilterIsNull()
        {
            // Act
            var result = await _reservaRepository.GetAllAsync(null);

            // Assert
            Assert.True(result.Success);
            Assert.Empty((List<Reserva>)result.Data);
        }

        [Fact]
        public async Task GetAllAsyncWithFilter_ShouldReturnSuccess_WhenFilterMatches()
        {
            // Arrange
            var reserva = CreateSampleReserva();
            await _reservaRepository.SaveEntityAsync(reserva);

            Expression<Func<Reserva, bool>> filter = r => r.IdCliente == reserva.IdCliente;

            // Act
            var result = await _reservaRepository.GetAllAsync(filter);

            // Assert
            Assert.True(result.Success);
            Assert.Single((List<Reserva>)result.Data);
        }

        [Fact]
        public async Task GetAllAsyncWithFilter_ShouldNotReturnDeletedReservas()
        {
            // Arrange
            var reserva = CreateSampleReserva();
            var saveResult = await _reservaRepository.SaveEntityAsync(reserva);
            var savedReserva = (Reserva)saveResult.Data;

            // Mark as deleted
            savedReserva.Borrado = true;
            savedReserva.UsuarioActualizacion = 1;
            await _reservaRepository.UpdateEntityAsync(savedReserva);

            Expression<Func<Reserva, bool>> filter = r => r.IdCliente == reserva.IdCliente;

            // Act
            var result = await _reservaRepository.GetAllAsync(filter);

            // Assert
            Assert.True(result.Success);
            Assert.Empty((List<Reserva>)result.Data);
        }
        #endregion

        #region "Helper Methods"
        private Reserva CreateSampleReserva()
        {
            return new Reserva
            {
                IdReserva = 2,
                IdCliente = 1,
                IdHabitacion = 1,
                FechaEntrada = new DateTime(2025, 3, 21, 0, 0, 0),
                FechaSalida = new DateTime(2025, 3, 23, 0, 0, 0),
                FechaSalidaConfirmacion = new DateTime(2025, 3, 23, 0, 0, 0),
                PrecioInicial = 100.00m,
                Adelanto = 30.00m,
                PrecioRestante = 70.00m,
                TotalPagado = 30.00m,
                CostoPenalidad = 0.00m,
                Observacion = "Test observation",
                NumeroHuespedes = 2,
                Estado = true,
                UsuarioCreacion = 1,
                FechaCreacion = new DateTime(2025, 3, 20, 0, 0, 0),
                Borrado = false,

                // Nuevas propiedades agregadas
                UsuarioActualizacion = null,
                FechaActualizacion = null,
                UsuarioEliminacion = null,
                FechaEliminado = null
            };
        }
        #endregion
    }
}