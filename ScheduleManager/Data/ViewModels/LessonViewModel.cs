using ScheduleManager.Data.Models;

namespace ScheduleManager.Data.ViewModels;

public class LessonViewModel
{
    public Lesson Lesson { get; set; } = null!;
    
    public TimeOnly Start { get; set; }
    public TimeOnly Duration { get; set; }
    public TimeOnly End => Start.Add(Duration.ToTimeSpan());
}
