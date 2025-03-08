using System.ComponentModel.DataAnnotations;

namespace TodoApp.API.Models.Identity;

public class RefreshToken
{
    [Key]
    public string Token { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public virtual ApplicationUser User { get; set; } = null!;
} 