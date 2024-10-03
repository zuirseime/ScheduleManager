﻿namespace ScheduleManager.Data.Repositories;

public interface ICreatable<T> where T : class
{
    public Task CreateSync(T entity);
}
