using BobsTacosBackend.Models;

namespace BobsTacosBackend.Repositories
{
    public interface IRepository
    {

        Task<IEnumerable<MenuItem>> GetMenuItems();
        Task<MenuItem> GetMenuItemById(int id);
      Task DeleteMenuItem(int id);
        Task UpdateMenuItem(MenuItem menuItem);

        Task CreateMenuItem(MenuItem menuItem);
    }
}
