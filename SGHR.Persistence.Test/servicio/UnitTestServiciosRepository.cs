using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.servicio;
using SGHR.Persistence.Context;
using SGHR.Persistence.Repositories.reserva;
using SGHR.Persistence.Repositories.servicio;
using SGHR.Persistence.Test.habitacion.Base;
using System.Linq.Expressions;
using Xunit;

namespace SGHR.Persistence.Test.servicio
{
    public class UnitTestServiciosRepository : BaseTest<ServiciosRepository>
    {
        private readonly ServiciosRepository _serviciosRepository;

        public UnitTestServiciosRepository()
        {
            _serviciosRepository = new ServiciosRepository(_context, _mockLogger.Object, _mockConfiguration.Object);
        }

        #region "SaveEntityAsync"

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityIsSaved()
        {
            // Arrange
            var servicio = new Servicios
            {
                Nombre = "Servicio Test",
                Descripcion = "Descripción de prueba",
                UsuarioCreacion = 1
            };

            // Act
            var result = await _serviciosRepository.SaveEntityAsync(servicio);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Datos Guardados Correctamente", result.Message);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenDescriptionIsEmpty()
        {
            // Arrange
            var servicio = new Servicios
            {
                Nombre = "Servicio Test",
                Descripcion = "",
                UsuarioCreacion = 1
            };

            // Act
            var result = await _serviciosRepository.SaveEntityAsync(servicio);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Error la descripcion es Requerida", result.Message);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenUserCreationIsInvalid()
        {
            // Arrange
            var servicio = new Servicios
            {
                Nombre = "Servicio Test",
                Descripcion = "Descripción de prueba",
                UsuarioCreacion = 0
            };

            // Act
            var result = await _serviciosRepository.SaveEntityAsync(servicio);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Error el Usuario de creacion tiene que ser valido, no puede ser menor que 0", result.Message);
        }

        #endregion

        #region "UpdateEntityAsync"

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsUpdated()
        {
            // Arrange
            var servicio = new Servicios
            {
                Nombre = "Servicio Original",
                Descripcion = "Descripción original",
                UsuarioCreacion = 1
            };
            var createResult = await _serviciosRepository.SaveEntityAsync(servicio);
            var savedService = (Servicios)createResult.Data;

            var updatedService = new Servicios
            {
                IdServicio = savedService.IdServicio,
                Nombre = "Servicio Actualizado",
                Descripcion = "Descripción actualizada",
                UsuarioActualizacion = 2,
                FechaActualizacion = DateTime.Now
            };

            // Act
            var result = await _serviciosRepository.UpdateEntityAsync(updatedService);

            // Assert
            Assert.True(result.Success);
            var resultService = (Servicios)result.Data;
            Assert.Equal("Servicio Actualizado", resultService.Nombre);
            Assert.Equal("Descripción actualizada", resultService.Descripcion);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Act
            var result = await _serviciosRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Error No Se Permite EntidadesNulas", result.Message);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenUserUpdateIsInvalid()
        {
            // Arrange
            var servicio = new Servicios
            {
                IdServicio = 1,
                Nombre = "Servicio Test",
                Descripcion = "Descripción de prueba",
                UsuarioActualizacion = 0
            };

            // Act
            var result = await _serviciosRepository.UpdateEntityAsync(servicio);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Error Actualizando El usuario Actualizacion deve se pasado", result.Message);
        }

        #endregion

        #region "GetEntityByIdAsync"

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnEntity_WhenIdExists()
        {
            // Arrange
            var servicio = new Servicios
            {
                Nombre = "Servicio Test",
                Descripcion = "Descripción de prueba",
                UsuarioCreacion = 1
            };
            var createResult = await _serviciosRepository.SaveEntityAsync(servicio);
            var savedService = (Servicios)createResult.Data;

            // Act
            var result = await _serviciosRepository.GetEntityByIdAsync(savedService.IdServicio);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(savedService.IdServicio, result.IdServicio);
            Assert.Equal("Servicio Test", result.Nombre);
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldThrowException_WhenIdIsInvalid()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _serviciosRepository.GetEntityByIdAsync(0));
            await Assert.ThrowsAsync<ArgumentException>(() => _serviciosRepository.GetEntityByIdAsync(-1));
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldThrowException_WhenEntityNotFound()
        {
            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviciosRepository.GetEntityByIdAsync(999));
        }

        #endregion

        #region "ExistsAsync"

        [Fact]
        public async Task ExistsAsync_ShouldReturnTrue_WhenEntityExists()
        {
            // Arrange
            var servicio = new Servicios
            {
                Nombre = "Servicio Existente",
                Descripcion = "Descripción de prueba",
                UsuarioCreacion = 1
            };
            await _serviciosRepository.SaveEntityAsync(servicio);

            // Act
            var result = await _serviciosRepository.ExistsAsync(s => s.Nombre == "Servicio Existente");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnFalse_WhenEntityDoesNotExist()
        {
            // Act
            var result = await _serviciosRepository.ExistsAsync(s => s.Nombre == "Servicio No Existente");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ExistsAsync_ShouldThrowException_WhenFilterIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _serviciosRepository.ExistsAsync(null));
        }

        #endregion

        #region "GetAllAsync with filter"

        [Fact]
        public async Task GetAllAsync_WithFilter_ShouldReturnFilteredEntities()
        {
            // Arrange
            await _serviciosRepository.SaveEntityAsync(new Servicios
            {
                Nombre = "Servicio A",
                Descripcion = "Descripción A",
                UsuarioCreacion = 1
            });

            await _serviciosRepository.SaveEntityAsync(new Servicios
            {
                Nombre = "Servicio B",
                Descripcion = "Descripción B",
                UsuarioCreacion = 1
            });

            // Act
            var result = await _serviciosRepository.GetAllAsync(s => s.Nombre == "Servicio A");

            // Assert
            Assert.True(result.Success);
            var services = (List<Servicios>)result.Data;
            Assert.Single(services);
            Assert.Equal("Servicio A", services[0].Nombre);
        }

        [Fact]
        public async Task GetAllAsync_WithNullFilter_ShouldReturnEmptyList()
        {
            // Act
            var result = await _serviciosRepository.GetAllAsync(null);

            // Assert
            Assert.True(result.Success);
            var services = (List<Servicios>)result.Data;
            Assert.Empty(services);
        }

        #endregion
    }
}