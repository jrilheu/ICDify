using System.ComponentModel.DataAnnotations;

namespace ICDify.Infrastructure.Persistence.Entities;

public class DrugEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = default!;

    public string? Ndc { get; set; }  // Optional: for National Drug Code

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public List<IndicationEntity> Indications { get; set; } = new();
}
