using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BobsTacosBackend.Data;

namespace BobsTacosBackend.Models
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }

        // Foreign key for the user who owns this wishlist
        [Required]
        public string UserId { get; set; }

        // Navigation property to the user who owns this wishlist
        public ApplicationUser User { get; set; }

        // Navigation property to the menu items in this wishlist
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        // Other properties of Wishlist

        // Method to add a MenuItem to the wishlist by its ID
        public void AddMenuItemById(int menuItemId, DatabaseContext dbContext)
        {
            var menuItem = dbContext.MenuItems.Find(menuItemId);
            if (menuItem != null)
            {
                MenuItems.Add(menuItem);
            }
            else
            {
                // Handle case when MenuItem with given ID is not found
            }
        }
    }
}
