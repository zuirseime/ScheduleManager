using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Data.Models;

public class Ring : Entity, IComparable<Ring>
{
    [Required]
    [DisplayName("Ring Number")]
    public byte Number { get; set; }

    [Required]
    [DisplayName("Ring Time")]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public TimeOnly Time { get; set; }

    public int CompareTo(Ring? other) => Time.CompareTo(other?.Time);
}
