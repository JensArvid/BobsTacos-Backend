using BobsTacosBackend.Models;

namespace BobsTacosBackend.Repositories
{
    public interface IRepository
    {

        Task<IEnumerable<MenuItem>> GetMenuItems();
        Task<MenuItem> GetMenuItemById(int id);
        Task<MenuItemDto> CreateMenuItem(MenuItemDto menuItemDto);


    }
}
