using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreeLibrary.Entity.Validation
{
    public static class ValidationExtension
    {
        public static EntityValidationResult Validate<T>(this T entity) where T : IBaseBO
        {
            EntityValidationResult result = null;
            Exception e = null;
            var validationResults = new List<ValidationResult>();

            try
            {
                var vc = new ValidationContext(entity, null, null);
                var isValid = Validator.TryValidateObject
                        (entity, vc, validationResults, true);
            }
            catch (Exception ex)
            {
                e = ex;
            }

            result = new EntityValidationResult(validationResults, e);

            return result;
        }


    }
}
