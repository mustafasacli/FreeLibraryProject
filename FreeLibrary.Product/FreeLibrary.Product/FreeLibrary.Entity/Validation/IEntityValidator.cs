namespace FreeLibrary.Entity.Validation
{
    public interface IEntityValidator<T> where T : IBaseBO
    {
        EntityValidationResult Validate(T entity);
    }
}
