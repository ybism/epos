using System.Collections.Generic;
using System.Threading.Tasks;
using epos.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace epos.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategory(Category category);
        List<Category> GetCategory();
        Task<Category> UpdateCategory(Category category);
        Task<Category> DeleteCategory(long id);
        Task<Category> GetCategoryById(long id);
        Task<Category> AddSubcategoryToCategory(Category category, Subcategory subcategory);
        Task<Category> GetCategoryByName(string name);
        Task<Category> GetSubcategoriesForCategory(string name);
    }
}