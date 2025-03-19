using MedicalAppointment.Persistence.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHR.Application.DTos.habitacion.Categoria;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;

namespace SGHR.Application.Service.habitacion
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CategoriaService> _logger;

        public CategoriaService(ICategoriaRepository repository, IConfiguration configuration, ILogger<CategoriaService> logger)
        {
            _repository = repository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<OperationResult> GeAll()
        {
            var result = new OperationResult();
            try
            {
                var categories = await _repository.GetAllAsync();
                result.Success = true;
                result.Data = categories.Select(x => new CategoriaDTO
                {
                    IdCategoria = x.IdCategoria,
                    Descripcion = x.Descripcion,
                    Estado = x.Estado
                });
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetAllCategories"];
                _logger.LogError(ex, result.Message);
            }

            return result;
        }

        public async Task<OperationResult> GeById(int id)
        {
            var result = new OperationResult();
            try
            {
                var category = await _repository.GetEntityByIdAsync(id);
                if (category == null)
                {
                    result.Success = false;
                    result.Message = _configuration["CategoryNotFound"];
                    return result;
                }

                result.Success = true;
                result.Data = new CategoriaDTO()
                {
                    IdCategoria = category.IdCategoria,
                    Descripcion = category.Descripcion,
                    Estado = category.Estado

                };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorGetCategoryById"];
                _logger.LogError(ex, result.Message);
            }

            return result;
        }

        public async Task<OperationResult> Remove(RemoveCategoriaDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var category = await _repository.GetEntityByIdAsync(dto.IdCategoria);
                if (category == null)
                {
                    result.Success = false;
                    result.Message = _configuration["CategoryNotFound"];
                    return result;
                }

                var tieneHabitacionAsignada = await _repository.GetHabitacionByCategoriaId(dto.IdCategoria);

                if (tieneHabitacionAsignada.Success)
                {
                    result.Success = false;
                    result.Message = "No es permitido eliminar categorias con habitaciones asignadas";
                    return result;
                }
                    
                category.Borrado = true;
                category.FechaEliminado = DateTime.UtcNow;
                category.UsuarioEliminacion = 0;
                category.Estado = false;

                await _repository.UpdateEntityAsync(category);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorRemoveCategory"];
                _logger.LogError(ex, result.Message);
            }

            return result;
        }

        public async Task<OperationResult> Save(SaveCategoriaDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var category = new Categoria
                {
                    FechaCreacion = DateTime.UtcNow,
                    IdServicio = dto.IdServicio,
                    Descripcion = dto.Descripcion,
                    Estado = dto.Estado,
                    UsuarioCreacion = 0,
                };

                var servicio = await _repository.ServicioExiste(dto.IdServicio);

                if (!servicio)
                {
                    result.Success = false;
                    result.Message = "El servicio no existe";
                    return result;
                }

                var isValid = await BaseValidator<Categoria>.ValidateEntityAsync(category);
                if (!isValid.Success)
                {
                    return isValid;
                }

                await _repository.SaveEntityAsync(category);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorSaveCategory"];
                _logger.LogError(ex, result.Message);
            }

            return result;
        }

        public async Task<OperationResult> Update(UpdateCategoriaDTO dto)
        {
            var result = new OperationResult();
            try
            {
                var category = await _repository.GetEntityByIdAsync(dto.IdCategoria);
                if (category == null)
                {
                    result.Success = false;
                    result.Message = _configuration["CategoryNotFound"];
                    return result;
                }


                var isValid = await BaseValidator<Categoria>.ValidateEntityAsync(category);
                if (!isValid.Success)
                {
                    return isValid;
                }

                category.FechaActualizacion = DateTime.UtcNow;
                category.IdServicio = dto.IdServicio;
                category.Descripcion = dto.Descripcion;
                category.Estado = dto.Estado;
                category.UsuarioActualizacion = 0;


                await _repository.UpdateEntityAsync(category);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _configuration["ErrorUpdateCategory"];
                _logger.LogError(ex, result.Message);
            }

            return result;
        }
    }
}
