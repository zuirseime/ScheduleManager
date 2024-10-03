using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Data.Models;

public class Ring
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [DisplayName("Ring Number")]
    public byte Number { get; set; }

    [Required]
    [DisplayName("Ring Time")]
    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public TimeOnly Time { get; set; }
}
