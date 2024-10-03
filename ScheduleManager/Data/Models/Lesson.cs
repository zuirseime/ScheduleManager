using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ScheduleManager.Data.Enums;

namespace ScheduleManager.Data.Models;

[DisplayColumn(nameof(Discipline))]
public class Lesson : Entity, IComparable<Lesson>
{
    [Required]
    [DisplayName("Day of Week")]
    public DayOfWeek Day { get; set; }

    [Required]
    [DisplayName("Lesson Number")]
    public byte LessonNumber { get; set; }

    [Required]
    [DisplayName("Repetition")]
    public Repetition Repetition { get; set; } = Repetition.Always;

    [Required]
    [DisplayName("Lesson Type")]
    public LessonType LessonType { get; set; }

    [Required]
    [DisplayName("Discipline")]
    public Guid DisciplineId { get; set; }
    public Discipline? Discipline { get; set; }

    [DisplayName("Teacher")]
    public string? Teacher { get; set; }

    public int CompareTo(Lesson? other) => Id.CompareTo(other?.Id);
}
