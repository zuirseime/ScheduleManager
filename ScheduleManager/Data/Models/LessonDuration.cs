using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ScheduleManager.Data.Enums;

namespace ScheduleManager.Data.Models;

public class LessonDuration : Entity, IComparable<LessonDuration>
{
    [Required]
    [DisplayName("Lesson Type")]
    public LessonType LessonType { get; set; }

    [Required]
    [DisplayName("Lesson Duration")]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public TimeOnly Duration { get; set; }

    public int CompareTo(LessonDuration? other) => Duration.CompareTo(other?.Duration);
}
