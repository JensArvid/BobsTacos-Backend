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

            MenuItemGroup.MapGet("/MenuItems", GetMenuItems);
            MenuItemGroup.MapGet("/MenuItems/{id}", GetMenuItemsById);
            MenuItemGroup.MapDelete("/MenuItems/{id}", DeleteMenuItem);
            MenuItemGroup.MapPut("/MenuItems/{id}", UpdateMenuItem);
            MenuItemGroup.MapPost("/MenuItems", CreateMenuItem);


        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetMenuItems(IRepository repository)
        {
            var menuitems = await repository.GetMenuItems();

            return TypedResults.Ok(menuitems);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetMenuItemsById(IRepository repository, int id)
        {
            var menuitems = await repository.GetMenuItemById(id);

            if (menuitems != null)
            {
                return TypedResults.Ok(menuitems);
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
        public static async Task<IResult> UpdateMenuItem(IRepository repository, int id, MenuItem menuItem)
        {
            menuItem.Id = id;
            await repository.UpdateMenuItem(menuItem);
            return Results.NoContent();
        }
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> CreateMenuItem(IRepository repository, MenuItem menuItem)
        {
            await repository.CreateMenuItem(menuItem);
            return Results.Created($"/MenuItems/{menuItem.Id}", menuItem);
        }


    }

}


