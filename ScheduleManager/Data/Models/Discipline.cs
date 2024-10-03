using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Data.Models;

[DisplayColumn(nameof(Name))]
public class Discipline : Entity, IComparable<Discipline>
{
    [Required]
    [DisplayName("Name")]
    public string Name { get; set; } = null!;

    public int CompareTo(Discipline? other) => Name.CompareTo(other?.Name);
}
