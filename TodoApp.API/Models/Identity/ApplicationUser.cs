using Microsoft.AspNetCore.Identity;

namespace TodoApp.API.Models.Identity;

public class ApplicationUser : IdentityUser
{
    public override string Email 
    { 
        get => base.UserName; 
        set
        {
            base.Email = value;
            base.UserName = value;
        }
    }
} 