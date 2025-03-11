
using SGHR.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointment.Persistence.Base
{
    public static class BaseValidator<TEntity>
    {
        public static async Task<OperationResult> ValidateNull(TEntity entity)
        {
            OperationResult operationResult = new OperationResult();
            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null";
                return operationResult;
            }
            return operationResult;
        }

        public static async Task<OperationResult> ValidateNullData(dynamic data)
        {
            OperationResult operationResult = new OperationResult();
            if (data == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The ID does not exist in the database";
                return operationResult;
            }
            return data;
        }

        public static async Task<OperationResult> ValidateID(int id)
        {
            OperationResult operationResult = new OperationResult();
            if (id <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "The ID must be positive or an integer";
                return operationResult;
            }
            return operationResult;
        }

        public static async Task<OperationResult> ValidateEntityAsync<TEntity>(TEntity entity)
        {
            OperationResult result = new OperationResult();
            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad no puede ser nula.";
                return result;
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(entity, null, null);
            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            if (!isValid)
            {
                result.Success = false;
                result.Message = "Errores de validación: " + string.Join(", ", validationResults.Select(v => v.ErrorMessage));
            }

            result.Success = true;
            return result;
        }
    }
}