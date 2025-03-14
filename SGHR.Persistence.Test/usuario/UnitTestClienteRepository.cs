using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Domain.Entities.usuario;
using SGHR.Persistence.Interfaces.usuario;
using SGHR.Persistence.Repositories.usuario;
using SGHR.Persistence.Test.habitacion.Base;


namespace SGHR.Persistence.Test;

public class UnitTestClienteRepository: BaseTest<ClienteRepository>
{
    private readonly IClienteRepository _clienteRepository;


    public UnitTestClienteRepository()
    {
        _clienteRepository = new ClienteRepository(_context, _mockLogger.Object, _mockConfiguration.Object);
    }

    #region "SaveEntityAsync"
    [Fact]
    public async Task SaveEntityAsync_ShouldReturnSucces_WhenEntityIsSaved()
    {
        //Arrange
        Cliente cliente = new Cliente()
        {
            IdCliente = 1,
            NombreCompleto = "Juan Emil",
            TipoDocumento = "DNI",
            Documento = "402102320",
            Correo = "Ahleandro18@gmail.com",
            Estado = true,
            Borrado = false
        };

        //Act 
        var result = await _clienteRepository.SaveEntityAsync(cliente);

        //Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task SaveEntityAsync_ShouldReturnFailure_WhenClienteIsNull() 
    {
        //Act 
        var result = await _clienteRepository.SaveEntityAsync(null);

        //Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task SaveEntityAsync_ShouldReturnFailure_WhenClientAlreadyExists()
    {
        //Arrange
        Cliente cliente = new Cliente()
        {
            IdCliente = 1,
            NombreCompleto = "Juan Emil",
            TipoDocumento = "DNI",
            Documento = "402102320",
            Correo = "Ahleandro18@gmail.com",
            Estado = true,
            Borrado = false
        };
        await _clienteRepository.SaveEntityAsync(cliente);


        //Act 
        var result = await _clienteRepository.SaveEntityAsync(cliente);

        //Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task SaveEntityAsync_ShouldReturnSuccess_WhenClienteParamethersIsNull() 
    {
        // Arrange
        var cliente = new Cliente();

        // Act
        var result = await _clienteRepository.SaveEntityAsync(cliente);

        // Assert
        Assert.False(result.Success);
    }
    #endregion

    #region "GetEntityById"
    [Fact]
    public async Task GetEntityById_ShouldReturnSuccess_WhenIdClienteIsCero() 
    {
        //Act
        var result = await _clienteRepository.GetEntityByIdAsync(0);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdClienteIsNegative()
    {
        // Act
        var result = await _clienteRepository.GetEntityByIdAsync(-1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdClienteExceedsTheRange()
    {
        // Act
        var result = await _clienteRepository.GetEntityByIdAsync(9999999);

        // Assert
        Assert.Null(result);
    }
    #endregion

    #region "GetAllAsync"
    [Fact]
    public async Task GetAllAsync_ShouldReturnSuccess_WhenDataExists()
    {
        // Arrange
        await _clienteRepository.SaveEntityAsync(new Cliente 
        {
            IdCliente = 1,
            NombreCompleto = "Juan Emil",
            TipoDocumento = "DNI",
            Documento = "402102320",
            Correo = "Ahleandro18@gmail.com",
            Estado = true,
            Borrado = false
        });
        // Act
        var result = await _clienteRepository.GetAllAsync();

        // Assert
        Assert.NotEmpty(result);
    }
    #endregion

    #region "GetClienteByReserva"
    [Fact]
    public async Task GetClienteByReserva_ShouldReturnSuccess_WhenCategoriaExists() 
    {
        // Arrange
        var cliente = new Cliente 
        {
            IdCliente = 1,
            NombreCompleto = "Juan Emil",
            TipoDocumento = "DNI",
            Documento = "402102320",
            Correo = "Ahleandro18@gmail.com",
            Estado = true,
            Borrado = false
        };
        await _clienteRepository.SaveEntityAsync(cliente);

        // Act
        var result = await _clienteRepository.GetCienteByReservas(cliente);

        // Assert
        Assert.NotNull(result);
    }
    #endregion

    #region "UpdateEntityAsync"
    [Fact]
    public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsUpdate()
    {
        // Arrange
        var cliente = new Cliente 
        {
            IdCliente = 1,
            NombreCompleto = "Juan Emil",
            TipoDocumento = "DNI",
            Documento = "402102320",
            Correo = "Ahleandro18@gmail.com",
            Estado = true,
            Borrado = false
        };
        await _clienteRepository.SaveEntityAsync(cliente);
        cliente.NombreCompleto = "Pedro Leandro Aponte";

        // Act
        var result = await _clienteRepository.UpdateEntityAsync(cliente);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsNull()
    {
        // Act
        var result = await _clienteRepository.UpdateEntityAsync(null);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenClienteParamethersIsNull()
    {
        // Arrange
        var cliente = new Cliente();

        // Act
        var result = await _clienteRepository.UpdateEntityAsync(cliente);

        // Assert
        Assert.False(result.Success);
    }
    #endregion
}
