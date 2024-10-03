using ScheduleManager.Data.Models;

namespace ScheduleManager.Services.Assignments;

public class AssignmentQueryService : IQueryService<Assignment>
{
    public async Task<IEnumerable<Assignment>> Sort(IEnumerable<Assignment> entities)
    {
        var sorted = Task.FromResult(entities.ToList());
        sorted.Result.Sort();
        return await sorted;
    }

    public async Task<IEnumerable<Assignment>> Find(IEnumerable<Assignment> entities, string query)
        => await Task.FromResult(entities.Where(e => e.Title.Contains(query)));

    public async Task<IEnumerable<Assignment>> Filter(IEnumerable<Assignment> entities, Func<Assignment, bool> predicate)
        => await Task.FromResult(entities.Where(predicate));
}
