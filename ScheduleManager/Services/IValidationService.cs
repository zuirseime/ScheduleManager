namespace ScheduleManager.Services;

public interface IValidationService<T> where T : class
{
    public Task<bool> ValidateAsync(T entity);
}
