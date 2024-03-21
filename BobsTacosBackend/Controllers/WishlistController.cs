using BobsTacosBackend.Data;
using BobsTacosBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BobsTacosBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public WishlistController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<WishlistDto>> CreateWishlistForUser([FromBody] CreateWishlistRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserId) || request.MenuItemId <= 0)
                {
                    return BadRequest("Invalid request parameters");
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                var menuItem = await _context.MenuItems.FindAsync(request.MenuItemId);
                if (menuItem == null)
                {
                    return NotFound("Menu item not found");
                }

                var wishlist = new Wishlist
                {
                    UserId = request.UserId
                };

                wishlist.MenuItems.Add(menuItem);
                _context.Wishlists.Add(wishlist);
                await _context.SaveChangesAsync();

                var wishlistDto = new WishlistDto
                {
                    Id = wishlist.Id,
                    UserId = wishlist.UserId,
                    MenuItems = wishlist.MenuItems.Select(m => new MenuItemDto
                    {
                        Id = m.id,
                        Name = m.name,
                        Price = m.price,
                        Rating = m.rating,
                        FoodType = m.foodType,
                        Description = m.description,
                        DeliveryTime = m.deliveryTime,
                        Image = m.image
                    }).ToList()
                };

                return CreatedAtAction(nameof(GetWishlist), new { id = wishlistDto.Id }, wishlistDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishlistDto>>> GetWishlists()
        {
            try
            {
                var wishlists = await _context.Wishlists
                    .Include(w => w.MenuItems)
                    .ToListAsync();

                var wishlistDtos = wishlists.Select(w => new WishlistDto
                {
                    Id = w.Id,
                    UserId = w.UserId,
                    MenuItems = w.MenuItems.Select(m => new MenuItemDto
                    {
                        Id = m.id,
                        Name = m.name,
                        Price = m.price,
                        Rating = m.rating,
                        FoodType = m.foodType,
                        Description = m.description,
                        DeliveryTime = m.deliveryTime,
                        Image = m.image
                    }).ToList()
                }).ToList();

                return Ok(wishlistDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("{id}")]
        public ActionResult<WishlistDto> GetWishlist(int id)
        {
            var wishlist = _context.Wishlists
                .Include(w => w.MenuItems)
                .FirstOrDefault(w => w.Id == id);

            if (wishlist == null)
            {
                return NotFound();
            }

            var wishlistDto = new WishlistDto
            {
                Id = wishlist.Id,
                UserId = wishlist.UserId,
                MenuItems = wishlist.MenuItems.Select(mi => new MenuItemDto
                {
                    Id = mi.id,
                    Name = mi.name,
                    Price = mi.price,
                    Rating = mi.rating,
                    FoodType = mi.foodType,
                    Description = mi.description,
                    DeliveryTime = mi.deliveryTime,
                    Image = mi.image
                }).ToList()
            };

            return wishlistDto;
        }

    }
    public class CreateWishlistRequest
    {
        public string UserId { get; set; }
        public int MenuItemId { get; set; }
    }
}
