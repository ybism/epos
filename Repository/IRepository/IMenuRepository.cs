using System.Collections.Generic;
using System.Threading.Tasks;
using epos.Models;

namespace epos.Repository.IRepository
{
    public interface IMenuRepository
    {
        Task<Menu> CreateMenu(Menu menu);
        List<Menu> GetMenu();
        Task<Menu> GetMenuById(long id);
        Task<Menu> UpdateMenu(Menu menu);
        Task DeleteMenu(long id);
        Task<Menu> AddCategoryToMenu(Menu menu, Category category);
        Task<Category> GetCategoryByName(string name);
        Task<Menu> GetMenuByName(string name);
        List<Category> GetCategoriesForMenu(string name);
    }
}