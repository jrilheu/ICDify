using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICDify.Infrastructure.Persistence.Entities;

public class IndicationEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Condition { get; set; } = default!;

    [Required]
    public string ICD10Code { get; set; } = default!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign Key
    [ForeignKey(nameof(Drug))]
    public Guid DrugId { get; set; }

    public DrugEntity Drug { get; set; } = default!;
}