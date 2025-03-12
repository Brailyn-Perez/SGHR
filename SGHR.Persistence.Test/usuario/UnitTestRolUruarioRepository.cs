using SGHR.Domain.Entities.usuario;
using SGHR.Persistence.Interfaces.usuario;
using SGHR.Persistence.Repositories.usuario;
using SGHR.Persistence.Test.habitacion.Base;

namespace SGHR.Persistence.Test;


public class UnitTestRolUruarioRepository : BaseTest<RolUsuarioRepository>
{
    private readonly IRolUsuarioRepository _rolUsuarioRepository;
  
    public UnitTestRolUruarioRepository()
    {
        _rolUsuarioRepository = new RolUsuarioRepository(_context, _mockLogger.Object, _mockConfiguration.Object);
    }

    #region "SaveEntityAsync"
    [Fact]
    public async Task SaveEntityAsync_ShouldReturnSucces_WhenEntityIsSaved()
    {
        //Arrange
        RolUsuario rolusuario = new RolUsuario()
        {
            IdRolUsuario = 1,
            Descripcion = "Seguridad",
            Estado = true,
            Borrado = false
        };

        //Act 
        var result = await _rolUsuarioRepository.SaveEntityAsync(rolusuario);

        //Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task SaveEntityAsync_ShouldReturnFailure_WhenRolUsuarioIsNull()
    {
        //Act 
        var result = await _rolUsuarioRepository.SaveEntityAsync(null);

        //Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task SaveEntityAsync_ShouldReturnFailure_WhenRolUsuarioAlreadyExists()
    {
        //Arrange
        RolUsuario rolusuario = new RolUsuario()
        {
            IdRolUsuario = 1,
            Descripcion = "Seguridad",
            Estado = true,
            Borrado = false
        };
        await _rolUsuarioRepository.SaveEntityAsync(rolusuario);


        //Act 
        var result = await _rolUsuarioRepository.SaveEntityAsync(rolusuario);

        //Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task SaveEntityAsync_ShouldReturnSuccess_WhenRolUsuarioParamethersIsNull()
    {
        // Arrange
        var rolusuario = new RolUsuario();

        // Act
        var result = await _rolUsuarioRepository.SaveEntityAsync(rolusuario);

        // Assert
        Assert.False(result.Success);
    }
    #endregion

    #region "GetEntityById"
    [Fact]
    public async Task GetEntityById_ShouldReturnSuccess_WhenIdRolUsuairoIsCero()
    {
        //Act
        var result = await _rolUsuarioRepository.GetEntityByIdAsync(0);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdRolUsuarioIsNegative()
    {
        // Act
        var result = await _rolUsuarioRepository.GetEntityByIdAsync(-1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdRolUsuarioExceedsTheRange()
    {
        // Act
        var result = await _rolUsuarioRepository.GetEntityByIdAsync(9999999);

        // Assert
        Assert.Null(result);
    }
    #endregion

    #region "GetAllAsync"
    [Fact]
    public async Task GetAllAsync_ShouldReturnSuccess_WhenDataExists()
    {
        // Arrange
        await _rolUsuarioRepository.SaveEntityAsync(new RolUsuario
        {
            IdRolUsuario = 1,
            Descripcion = "Seguridad",
            Estado = true,
            Borrado = false
        });
        // Act
        var result = await _rolUsuarioRepository.GetAllAsync();

        // Assert
        Assert.NotEmpty(result);
    }
    #endregion

    #region "UpdateEntityAsync"
    [Fact]
    public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsUpdate()
    {
        // Arrange
        var rolusuario = new RolUsuario
        {
            IdRolUsuario = 1,
            Descripcion = "Seguridad",
            Estado = true,
            Borrado = false
        };
        await _rolUsuarioRepository.SaveEntityAsync(rolusuario);
        rolusuario.Descripcion = "Limpieza";

        // Act
        var result = await _rolUsuarioRepository.UpdateEntityAsync(rolusuario);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsNull()
    {
        // Act
        var result = await _rolUsuarioRepository.UpdateEntityAsync(null);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenRolUsuarioParamethersIsNull()
    {
        // Arrange
        var rolusuario = new RolUsuario();

        // Act
        var result = await _rolUsuarioRepository.UpdateEntityAsync(rolusuario);

        // Assert
        Assert.False(result.Success);
    }
    #endregion
}
