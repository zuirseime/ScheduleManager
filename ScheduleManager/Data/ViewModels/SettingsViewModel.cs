using ScheduleManager.Data.Models;

namespace ScheduleManager.Data.ViewModels;

public class SettingsViewModel
{
    public IEnumerable<LessonDuration> LessonDurations { get; set; } = [];
    public IEnumerable<Ring> Rings { get; set; } = [];
}