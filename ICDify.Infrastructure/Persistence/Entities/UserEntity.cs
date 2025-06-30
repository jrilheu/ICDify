using System.ComponentModel.DataAnnotations;

namespace ICDify.Infrastructure.Persistence.Entities;

public class UserEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public string PasswordHash { get; set; } = default!;

    public string Role { get; set; } = "User";  // Roles: User, Admin
}
