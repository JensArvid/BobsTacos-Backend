using BobsTacosBackend.Enums;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace BobsTacosBackend.Models
{

    public class ApplicationUser : IdentityUser
        {
            public Role Role { get; set; }
        }
    
}
