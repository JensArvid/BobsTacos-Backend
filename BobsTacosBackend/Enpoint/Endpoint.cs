using BobsTacosBackend.Models;
using BobsTacosBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BobsTacosBackend.Enpoint
{
    public static class Endpoint
    {

        public static void ConfigureMenuItemEndpoint(this WebApplication app)
        {
            var MenuItemGroup = app.MapGroup("MenuItem");

            MenuItemGroup.MapGet("/", GetMenuItems);
            MenuItemGroup.MapGet("/{id}/", GetMenuItemsById);
            MenuItemGroup.MapDelete("/delete/{id}", DeleteMenuItem);
            MenuItemGroup.MapPut("/put/{id}", UpdateMenuItem);
            MenuItemGroup.MapPost("/post/", CreateMenuItem);

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IEnumerable<MenuDto>> GetMenuItems(IRepository repository)
        {
            var menuItems = await repository.GetMenuItems();
            var menuDtos = new List<MenuDto>();

            foreach (var menuItem in menuItems)
            {
                var menuDto = new MenuDto
                {
                    id = menuItem.id,
                    name = menuItem.name,
                    price = menuItem.price,
                    rating = menuItem.rating,
                    foodType = menuItem.foodType,
                    description = menuItem.description,
                    deliveryTime = menuItem.deliveryTime,
                    image = menuItem.image
                };

                menuDtos.Add(menuDto);
            }

            return menuDtos;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetMenuItemsById(IRepository repository, int id)
        {
            var menuItem = await repository.GetMenuItemById(id);

            if (menuItem != null)
            {
                var menuDto = new MenuDto
                {
                    id = menuItem.id,
                    name = menuItem.name,
                    price = menuItem.price,
                    rating = menuItem.rating,
                    foodType = menuItem.foodType,
                    description = menuItem.description,
                    deliveryTime = menuItem.deliveryTime,
                    image = menuItem.image
                };

                return TypedResults.Ok(menuDto);
            }
            else
            {
                return Results.NotFound($"MenuItem with ID {id} not found.");
            }
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteMenuItem(IRepository repository, int id)
        {
            await repository.DeleteMenuItem(id);
            return Results.NoContent();
        }
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateMenuItem(IRepository repository, int id, MenuDto menuDto)
        {
            var existingMenuItem = await repository.GetMenuItemById(id);
            if (existingMenuItem == null)
            {
                return Results.NotFound($"MenuItem with ID {id} not found.");
            }

            // Update the existing menu item with data from the DTO
            existingMenuItem.name = menuDto.name;
            existingMenuItem.price = menuDto.price;
            existingMenuItem.rating = menuDto.rating;
            existingMenuItem.foodType = menuDto.foodType;
            existingMenuItem.description = menuDto.description;
            existingMenuItem.deliveryTime = menuDto.deliveryTime;
            existingMenuItem.image = menuDto.image;

            // Call repository method to update the menu item
            await repository.UpdateMenuItem(existingMenuItem);

            return Results.NoContent();
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> CreateMenuItem(IRepository repository, MenuDto menuDto)
        {
            // Map the MenuDto to a MenuItem entity
            var menuItem = new MenuItem
            {
                id = menuDto.id,
                name = menuDto.name,
                price = menuDto.price,
                rating = menuDto.rating,
                foodType = menuDto.foodType,
                description = menuDto.description,
                deliveryTime = menuDto.deliveryTime,
                image = menuDto.image
            };

            // Call the repository method to create the menu item
            await repository.CreateMenuItem(menuItem);

            return Results.Created($"/MenuItems/{menuItem.id}", menuItem);
        }



    }

}


