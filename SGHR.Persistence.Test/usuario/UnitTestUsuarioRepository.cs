using SGHR.Domain.Entities.usuario;
using SGHR.Persistence.Interfaces.usuario;
using SGHR.Persistence.Repositories.usuario;
using SGHR.Persistence.Test.habitacion.Base;

namespace SGHR.Persistence.Test;


public class UnitTestUsuarioRepository : BaseTest<UsuarioRepository>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UnitTestUsuarioRepository()
    {
        _usuarioRepository = new UsuarioRepository(_context, _mockLogger.Object, _mockConfiguration.Object);
    }

    #region "SaveEntityAsync"
    [Fact]
    public async Task SaveEntityAsync_ShouldReturnSucces_WhenEntityIsSaved()
    {
        //Arrange
        Usuario usuario = new Usuario()
        {
            IdUsuario = 1,
            NombreCompleto = "Miguel",
            Correo = "ahleandro18@gmail.com",
            Clave = "Clave123",
            Estado = true,
            IdRolUsuario = 1,
            Borrado = false
        };

        //Act 
        var result = await _usuarioRepository.SaveEntityAsync(usuario);

        //Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task SaveEntityAsync_ShouldReturnFailure_WhenUsuarioIsNull()
    {
        //Act 
        var result = await _usuarioRepository.SaveEntityAsync(null);

        //Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task SaveEntityAsync_ShouldReturnFailure_WhenUsuarioAlreadyExists()
    {
        //Arrange
        Usuario usuario = new Usuario()
        {
            IdUsuario = 1,
            NombreCompleto = "Miguel",
            Correo = "ahleandro18@gmail.com",
            Clave = "Clave123",
            Estado = true,
            IdRolUsuario = 1,
            Borrado = false
        };
        await _usuarioRepository.SaveEntityAsync(usuario);


        //Act 
        var result = await _usuarioRepository.SaveEntityAsync(usuario);

        //Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task SaveEntityAsync_ShouldReturnSuccess_WhenUsuarioParamethersIsNull()
    {
        // Arrange
        var usuario = new Usuario();

        // Act
        var result = await _usuarioRepository.SaveEntityAsync(usuario);

        // Assert
        Assert.False(result.Success);
    }
    #endregion

    #region "GetEntityById"
    [Fact]
    public async Task GetEntityById_ShouldReturnSuccess_WhenIdUsuairoIsCero()
    {
        //Act
        var result = await _usuarioRepository.GetEntityByIdAsync(0);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdUsuarioIsNegative()
    {
        // Act
        var result = await _usuarioRepository.GetEntityByIdAsync(-1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdUsuarioExceedsTheRange()
    {
        // Act
        var result = await _usuarioRepository.GetEntityByIdAsync(9999999);

        // Assert
        Assert.Null(result);
    }
    #endregion

    #region "GetAllAsync"
    [Fact]
    public async Task GetAllAsync_ShouldReturnSuccess_WhenDataExists()
    {
        // Arrange
        await _usuarioRepository.SaveEntityAsync(new Usuario
        {
            IdUsuario = 1,
            NombreCompleto = "Miguel",
            Correo = "ahleandro18@gmail.com",
            Clave = "Clave123",
            Estado = true,
            IdRolUsuario = 1,
            Borrado = false
        });
        // Act
        var result = await _usuarioRepository.GetAllAsync();

        // Assert
        Assert.NotEmpty(result);
    }
    #endregion

    #region "UpdateEntityAsync"
    [Fact]
    public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsUpdate()
    {
        // Arrange
        var usuario = new Usuario
        {
            IdUsuario = 1,
            NombreCompleto = "Miguel",
            Correo = "ahleandro18@gmail.com",
            Clave = "Clave123",
            Estado = true,
            IdRolUsuario = 1,
            Borrado = false
        };
        await _usuarioRepository.SaveEntityAsync(usuario);
        usuario.NombreCompleto = "Pedro Leandro Aponte";

        // Act
        var result = await _usuarioRepository.UpdateEntityAsync(usuario);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsNull()
    {
        // Act
        var result = await _usuarioRepository.UpdateEntityAsync(null);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenUsuarioParamethersIsNull()
    {
        // Arrange
        var usuario = new Usuario();

        // Act
        var result = await _usuarioRepository.UpdateEntityAsync(usuario);

        // Assert
        Assert.False(result.Success);
    }
    #endregion
}
