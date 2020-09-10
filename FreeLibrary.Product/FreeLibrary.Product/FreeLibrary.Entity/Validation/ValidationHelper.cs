
namespace FreeLibrary.Entity.Validation
{
    public class ValidationHelper
    {
        public static IEntityValidationResult ValidateEntity<T>(T entity)
            where T : IBaseBO
        {
            IEntityValidationResult result = null;

            EntityValidator<T> validator = new EntityValidator<T>();

            result = validator.Validate(entity);

            return result;
        }
    }
}