using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using epos.DAL;
using epos.Models;
using epos.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace epos.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FoodItemContext _db;

        public CategoryRepository(FoodItemContext db)
        {
            _db = db;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            await _db.Categories.AddAsync(category);
            await Save();

            return category;
        }

        public async Task<Category> AddSubcategoryToCategory(Category category, Subcategory subcategory)
        {
            var categoryToUpdate = await _db.Categories.FirstOrDefaultAsync(x => x.Name == category.Name);
            var subcategoryToAdd = await _db.Subcategories.FirstOrDefaultAsync(x => x.Name == subcategory.Name);

            if (categoryToUpdate == null)
            {
                throw new Exception("Category not found");
            }

            if (subcategoryToAdd == null)
            {
                var newSubcategory = new Subcategory(subcategory.Name);

                categoryToUpdate.Subcategories.Add(newSubcategory);

                await _db.Subcategories.AddAsync(newSubcategory);
                await Save();

                return categoryToUpdate;
            }

            categoryToUpdate.Subcategories.Add(subcategoryToAdd);

            await Save();

            return categoryToUpdate;
        }

        public List<Category> GetCategory()
        {
            return _db.Categories.ToList();
        }

        public async Task<Category> GetCategoryById(long id)
        {
            return await _db.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _db.Categories.Where(x => x.Name == name).Include(m => m.Subcategories).FirstOrDefaultAsync();
        }

        public async Task<Category> GetSubcategoriesForCategory(string name)
        {
            return await _db.Categories.Where(x => x.Name == name).Include(m => m.Subcategories).FirstOrDefaultAsync();
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var categoryInDb = await _db.Categories.FirstOrDefaultAsync(x => x.Name == category.Name);

            if (categoryInDb == null)
            {
                throw new NullReferenceException("Category doesnt exist");
            }

            categoryInDb.Name = category.Name;

            _db.Categories.Update(categoryInDb);
           await Save();

            return category;
        }

        public async Task<Category> DeleteCategory(long id)
        {
            var categoryToDelete = await _db.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);

            if(categoryToDelete == null)
            {
                throw new NullReferenceException("Category does not exist");
            }

            _db.Categories.Remove(categoryToDelete);
            await Save();

            return categoryToDelete;
        }

        private async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}