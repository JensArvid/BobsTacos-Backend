using BobsTacosBackend.Repositories;
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
            var menuitems = await repository.GetMenuItemsById(id);

            if (menuitems != null)
            {
                return TypedResults.Ok(menuitems);
            }
            else
            {
                return Results.NotFound($"MenuItem with ID {id} not found.");
            }
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateMenuItem(IRepository repository, MenuItemDto createPatientDto)
        {
            var createdmenuitem = await repository.CreatePatient(createPatientDto);

            if (createdmenuitem != null)
            {
                return TypedResults.Ok(createdmenuitem);
            }
            else
            {
                return Results.BadRequest("Failed to create the MenuItem.");
            }
        }

        
    }

}


