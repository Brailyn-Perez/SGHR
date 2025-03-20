using SGHR.Domain.Base;
using SGHR.Domain.Entities.servicio;
using SGHR.Persistence.Repositories.servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGHR.Persistence.Base.ValidateRepository.ValidateHandlerServise
{
    public class ServiseNotNullProperties : IValidatorHandler<Servicios>
    {
        private IValidatorHandler<Servicios>? _next;

        public IValidatorHandler<Servicios>? SetNext(IValidatorHandler<Servicios> next)
        {
            _next = next;
            return _next;
        }

        public OperationResult Handler(Servicios servicio)
        {
            OperationResult result = new();

            //if (string.IsNullOrWhiteSpace(servicio.Nombre))
            //{
            //    result.Success = false;
            //    result.Message = "Error El nombre es Requerido";
            //    return result;
            //}
            if(servicio.Nombre.Length < 1 || servicio.Nombre.Length > 200)
            {
                result.Success = false;
                result.Message = "Error el nombre debe tener entre 1 y 200 caracteres";
                return result;
            }

            if(string.IsNullOrWhiteSpace(servicio.Descripcion))
            {
                result.Success = false;
                result.Message = "Error la descripcion es Requerida";
                return result;
            }
            if(servicio.Descripcion.Length < 1 || servicio.Descripcion.Length > 10000)
            {
                result.Success = false;
                result.Message = "Error la descripcion deve tener entre 1 y 10000 caracteres";
                return result;
            }
            
            if(servicio.UsuarioCreacion <= 0)
            {
                result.Success = false;
                result.Message = "Error el Usuario de creacion tiene que ser valido, no puede ser menor que 0";
                return result;
            }
            return _next?.Handler(servicio) ?? result;
        }
    }
}
