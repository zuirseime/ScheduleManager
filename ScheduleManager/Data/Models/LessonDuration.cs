using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ScheduleManager.Data.Enums;

namespace ScheduleManager.Data.Models;

public class LessonDuration
{
    [Key]
    [Required]
    [DisplayName("Lesson Type")]
    public LessonType LessonType { get; set; }

    [Required]
    [DisplayName("Lesson Duration")]
    [DisplayFormat(DataFormatString = "{0:HH\\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan Duration { get; set; }
}
