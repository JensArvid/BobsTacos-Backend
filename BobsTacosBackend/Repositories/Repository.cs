using BobsTacosBackend.Data;
using BobsTacosBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BobsTacosBackend.Repositories
{
    public class Repository : IRepository
    {
        private DatabaseContext _databaseContext;
        public Repository(DatabaseContext db)
        {
            _databaseContext = db;
        }

        public async Task<MenuItem> GetMenuItemById(int id)
        {
            return await _databaseContext.MenuItems.FindAsync(id);
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItems()
        {
            return await _databaseContext.MenuItems.ToListAsync();
        }

        public async Task DeleteMenuItem(int id)
        {
            var menuItem = await _databaseContext.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _databaseContext.MenuItems.Remove(menuItem);
                await _databaseContext.SaveChangesAsync();
            }
        }



        public async Task UpdateMenuItem(MenuItem menuItem)
        {
            _databaseContext.MenuItems.Update(menuItem);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task CreateMenuItem(MenuItem menuItem)
        {
            await _databaseContext.MenuItems.AddAsync(menuItem);
            await _databaseContext.SaveChangesAsync();
        }


    }
}
