using BobsTacosBackend.Enums;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace BobsTacosBackend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation property for wishlist (one-to-one relationship)
        //public Wishlist Wishlist { get; set; }
        public ICollection<MenuItem> FavoriteMenuItems { get; set; }
    }
}

