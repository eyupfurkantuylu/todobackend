using System.ComponentModel.DataAnnotations;

namespace TodoApp.API.Dtos.Auth;

public class LoginDto
{
    [Required(ErrorMessage = "E-posta adresi zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur")]
    public string Password { get; set; } = string.Empty;
} 