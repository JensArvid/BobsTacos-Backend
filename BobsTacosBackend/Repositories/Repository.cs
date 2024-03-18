using BobsTacosBackend.Data;
using BobsTacosBackend.Models;

namespace BobsTacosBackend.Repositories
{
    public class Repository : IRepository
    {
        private DatabaseContext _databaseContext;
        public Repository(DatabaseContext db)
        {
            _databaseContext = db;
        }
        public Task<MenuItemDto> CreateMenuItem(MenuItemDto menuItemDto)
        {
            throw new NotImplementedException();
        }

        public Task<MenuItem> GetMenuItemById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MenuItem>> GetMenuItems()
        {
            throw new NotImplementedException();
        }
    }
}
