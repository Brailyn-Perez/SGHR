using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHR.Application.DTos.servicio.Servicio;
using SGHR.Application.Interfaces.sevicio;
using SGHR.Application.Service.servicio;
using SGHR.Application.Test.Base;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.servicio;
using SGHR.Persistence.Interfaces.servicio;
using System.Collections.Generic;
using Xunit;

namespace SGHR.Application.Test.servicio
{
    public class UnitTestServicioService : BaseTestService<IServiciosService, IServicioRepository>
    {
        private readonly ServicioService _servicioService;

        public UnitTestServicioService()
        {
            _servicioService = new ServicioService(
                _mockRepository.Object,
                _mockConfiguration.Object,
                _mockLogger.Object);
        }

        #region "GeAll"

        [Fact]
        public async Task GeAll_ShouldReturnSuccess_WhenRepositoryReturnsData()
        {
            // Arrange
            var servicios = new List<Servicios>
            {
                new Servicios { IdServicio = 1, Nombre = "Servicio 1", Descripcion = "Descripción 1" },
                new Servicios { IdServicio = 2, Nombre = "Servicio 2", Descripcion = "Descripción 2" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(servicios);

            // Act
            var result = await _servicioService.GeAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(servicios, result.Data);
            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GeAll_ShouldReturnFailure_WhenRepositoryThrowsException()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _servicioService.GeAll();

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
            var servicio = new Servicios
            {
                IdServicio = 1,
                Nombre = "Servicio Test",
                Descripcion = "Descripción de prueba"
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(servicio);

            // Act
            var result = await _servicioService.GeById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(servicio, result.Data);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GeById_ShouldReturnFailure_WhenRepositoryThrowsException()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _servicioService.GeById(1);

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
            var dto = new SaveServicioDTO
            {
                IdServicio = 1,
                Nombre = "Nuevo Servicio",
                Descripcion = "Nueva descripción"
            };

            var operationResult = new OperationResult
            {
                Success = true,
                Message = "Datos Guardados Correctamente",
                Data = new Servicios { IdServicio = 1 }
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Servicios>()))
                .ReturnsAsync(operationResult);

            // Act
            var result = await _servicioService.Save(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(operationResult, result.Data);
            _mockRepository.Verify(repo => repo.SaveEntityAsync(It.IsAny<Servicios>()), Times.Once);
        }

        [Fact]
        public async Task Save_ShouldReturnFailure_WhenRepositoryReturnsFail()
        {
            // Arrange
            var dto = new SaveServicioDTO
            {
                IdServicio = 1,
                Nombre = "Nuevo Servicio",
                Descripcion = "Nueva descripción"
            };

            var operationResult = new OperationResult
            {
                Success = false,
                Message = "Error al guardar"
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Servicios>()))
                .ReturnsAsync(operationResult);

            // Act
            var result = await _servicioService.Save(dto);

            // Assert
            Assert.True(result.Success);
            _mockRepository.Verify(repo => repo.SaveEntityAsync(It.IsAny<Servicios>()), Times.Once);
        }

        #endregion

        #region "Update"

        [Fact]
        public async Task Update_ShouldReturnSuccess_WhenEntityIsUpdated()
        {
            // Arrange
            var dto = new UpdateServicioDTO
            {
                IdServicio = 1,
                Nombre = "Servicio Actualizado",
                Descripcion = "Descripción actualizada"
            };

            var existingEntity = new Servicios
            {
                IdServicio = 1,
                Nombre = "Servicio Original",
                Descripcion = "Descripción original"
            };

            var operationResult = new OperationResult
            {
                Success = true,
                Message = "Servicio actualizadoCorrectamente",
                Data = new Servicios { IdServicio = 1 }
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(existingEntity);

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Servicios>()))
                .ReturnsAsync(operationResult);

            // Act
            var result = await _servicioService.Update(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(operationResult, result.Data);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(1), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.IsAny<Servicios>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnFailure_WhenEntityDoesNotExist()
        {
            // Arrange
            var dto = new UpdateServicioDTO
            {
                IdServicio = 999,
                Nombre = "Servicio Actualizado",
                Descripcion = "Descripción actualizada"
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Servicios)null);

            // Act
            var result = await _servicioService.Update(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El servicio no existe.", result.Message);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(999), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.IsAny<Servicios>()), Times.Never);
        }

        #endregion

        #region "Remove"

        [Fact]
        public async Task Remove_ShouldReturnSuccess_WhenEntityIsRemoved()
        {
            // Arrange
            var dto = new RemoveServicioDTO
            {
                IdServicio = 1
            };

            var existingEntity = new Servicios
            {
                IdServicio = 1,
                Nombre = "Servicio a eliminar",
                Descripcion = "Descripción"
            };

            var operationResult = new OperationResult
            {
                Success = true,
                Message = "Entidad eliminada correctamente",
                Data = existingEntity
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(existingEntity);

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Servicios>()))
                .ReturnsAsync(operationResult);

            // Act
            var result = await _servicioService.Remove(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(operationResult, result.Data);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(1), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<Servicios>(s => s.Borrado == true)), Times.Once);
        }

        [Fact]
        public async Task Remove_ShouldReturnFailure_WhenRepositoryThrowsException()
        {
            // Arrange
            var dto = new RemoveServicioDTO
            {
                IdServicio = 1
            };

            var existingEntity = new Servicios
            {
                IdServicio = 1,
                Nombre = "Servicio a eliminar",
                Descripcion = "Descripción"
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(existingEntity);

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Servicios>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _servicioService.Remove(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Mock error message", result.Message);
            _mockRepository.Verify(repo => repo.GetEntityByIdAsync(1), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<Servicios>(s => s.Borrado == true)), Times.Once);
        }

        #endregion
    }
}