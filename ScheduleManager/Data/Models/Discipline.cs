using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Data.Models;

[DisplayColumn(nameof(Name))]
public class Discipline
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [DisplayName("Name")]
    public string Name { get; set; } = null!;
}
