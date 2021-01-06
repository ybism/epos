using epos.Models;
using epos.Repository.IRepository;
using epos.DAL;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace epos.Repository
{
    public class MenuRepository : IMenuRepository
    {

        private readonly FoodItemContext _db;

        public MenuRepository(FoodItemContext db)
        {
            _db = db;
        }

        public async Task<Menu> CreateMenu(Menu menu)
        {
            await _db.Menus.AddAsync(menu);
            await Save();

            return menu;
        }

        public async Task<Menu> AddCategoryToMenu(Menu menu, Category category)
        {
            var menuToUpdate = await _db.Menus.FirstOrDefaultAsync(x => x.Name == menu.Name);
            var Category = await _db.Categories.FirstOrDefaultAsync(x => x.Name == category.Name);

            if (Category == null)
            {
                Category NewCategory = new Category(category.Name);
                menu.Categories.Add(NewCategory);

                await _db.Categories.AddAsync(NewCategory);
                await Save();


                return menu;
            }

            menu.Categories.Add(Category);
            await Save();

            return menu;
        }

        public async Task<Menu> GetMenuByName(string name)
        {
            return await _db.Menus.Where(x => x.Name == name).Include(m => m.Categories).FirstOrDefaultAsync();
        }

        public async Task<Menu> GetMenuById(long id)
        {
            var menu = await _db.Menus.FirstOrDefaultAsync(x => x.Id == id);
            return menu;
        }
        public async Task<Category> GetCategoryById(string name)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Name == name);
            return category;
        }

        public List<Category> GetCategoriesForMenu(string name)
        {
            var MenuInDb = _db.Menus.FirstOrDefault(x => x.Name == name);

            return MenuInDb?.Categories.ToList();
        }

        public List<Menu> GetMenu()
        {
            return _db.Menus.Include(m => m.Categories).ToList();
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Name == name);
            return category;
        }

        public async Task<Menu> UpdateMenu(Menu menu)
        {
            var MenuInDb = await _db.Menus.FirstOrDefaultAsync(x => x.Id == menu.Id);
            if (MenuInDb == null)
            {
                throw new NullReferenceException("Menu does not exist");
            }

            MenuInDb.Name = menu.Name;
            _db.Menus.Update(menu);
            await Save();

            return menu;

        }

        public async Task DeleteMenu(long id)
        {
            var MenuInDb = await _db.Menus.FirstOrDefaultAsync(x => x.Id == id);
            if (MenuInDb == null)
            {
                throw new NullReferenceException();
            }

            _db.Menus.Remove(MenuInDb);
            await Save();
        }

        private async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
