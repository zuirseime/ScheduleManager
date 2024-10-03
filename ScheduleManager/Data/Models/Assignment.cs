using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ScheduleManager.Data.Enums;

namespace ScheduleManager.Data.Models;

[DisplayColumn(nameof(Title))]
public class Assignment : Entity, IComparable<Assignment>
{
    [Required]
    [DisplayName("Title")]
    public string Title { get; set; } = null!;

    [Required]
    [DisplayName("Discipline")]
    public Guid DisciplineId { get; set; }
    public Discipline? Discipline { get; set; }

    [DisplayName("Description")]
    public string? Description { get; set; }

    [Required]
    [DisplayName("Due Date")]
    public DateOnly DueDate { get; set; }

    [DisplayName("Is done")]
    public bool IsDone { get; set; } = false;

    [Required]
    [DisplayName("Assignment Type")]
    public AssignmentType Type { get; set; }

    public int CompareTo(Assignment? other)
    {
        if (other is null) return 1;

        return DueDate.CompareTo(other.DueDate);
    }
}
