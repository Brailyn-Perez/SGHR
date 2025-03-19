//using Xunit;
//using Moq;
//using SGHR.Application.Service.servicio;
//using SGHR.Domain.Entities.servicio;
//using SGHR.Persistence.Repositories;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//public class ServicioServiceTests
//{
//    private readonly Mock<IServicioRespository> _repositoryMock;
//    private readonly Mock<IConfiguration> _configurationMock;
//    private readonly Mock<ILogger<ServicioRespository>> _loggerMock;
//    private readonly ServicioService _service;

//    public ServicioServiceTests()
//    {
//        _repositoryMock = new Mock<IServicioRespository>();
//        _configurationMock = new Mock<IConfiguration>();
//        _loggerMock = new Mock<ILogger<ServicioRespository>>();
//        _service = new ServicioService(_repositoryMock.Object, _configurationMock.Object, _loggerMock.Object);
//    }

//    [Fact]
//    public async Task GeAll_ShouldReturnAllServices()
//    {
//        // Arrange
//        var services = new List<Servicios> { new Servicios { IdServicio = 1, Nombre = "Test", Descripcion = "Test Desc" } };
//        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(services);

//        // Act
//        var result = await _service.GeAll();

//        // Assert
//        Assert.True(result.Success);
//        Assert.Equal(services, result.Data);
//    }

//    [Fact]
//    public async Task GeById_ShouldReturnServiceById()
//    {
//        // Arrange
//        var service = new Servicios { IdServicio = 1, Nombre = "Test", Descripcion = "Test Desc" };
//        _repositoryMock.Setup(r => r.GetEntityByIdAsync(1)).ReturnsAsync(service);

//        // Act
//        var result = await _service.GeById(1);

//        // Assert
//        Assert.True(result.Success);
//        Assert.Equal(service, result.Data);
//    }

//    [Fact]
//    public async Task Save_ShouldSaveService()
//    {
//        // Arrange
//        var dto = new SaveServicioDTO { Nombre = "Test", Descripcion = "Test Desc" };
//        var service = new Servicios { IdServicio = 1, Nombre = dto.Nombre, Descripcion = dto.Descripcion };
//        _repositoryMock.Setup(r => r.SaveEntityAsync(It.IsAny<Servicios>())).ReturnsAsync(service);

//        // Act
//        var result = await _service.Save(dto);

//        // Assert
//        Assert.True(result.Success);
//        Assert.Equal(service, result.Data);
//    }

//    [Fact]
//    public async Task Update_ShouldUpdateService()
//    {
//        // Arrange
//        var dto = new UpdateServicioDTO { IdServicio = 1, Nombre = "Updated", Descripcion = "Updated Desc" };
//        var existingService = new Servicios { IdServicio = 1, Nombre = "Old", Descripcion = "Old Desc" };
//        _repositoryMock.Setup(r => r.GetEntityByIdAsync(dto.IdServicio)).ReturnsAsync(existingService);
//        _repositoryMock.Setup(r => r.UpdateEntityAsync(It.IsAny<Servicios>())).ReturnsAsync(existingService);

//        // Act
//        var result = await _service.Update(dto);

//        // Assert
//        Assert.True(result.Success);
//    }
//}