using ScheduleManager.Data.Models;

namespace ScheduleManager.Data.ViewModels;

public class AssignmentsViewModel
{
    public IEnumerable<Assignment> Assignments { get; set; } = null!;
    public int Total => Assignments.Count();
    public int Done => Assignments.Where(a => a.IsDone).Count();
    public double Progress => Math.Round(Done / (float)Total * 100, 2);

}
