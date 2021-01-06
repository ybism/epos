using System.Collections.Generic;
using epos.DAL;
using epos.Models;
using epos.Repository.IRepository;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace epos.Repository
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly FoodItemContext _db;

        public SubcategoryRepository(FoodItemContext db)
        {
            _db = db;
        }

        public async Task<Subcategory> CreateSubcategory(Subcategory subcategory)
        {
            await _db.Subcategories.AddAsync(subcategory);
            await Save();

            return subcategory;
        }

        public List<Subcategory> GetSubcategories()
        {
            return _db.Subcategories.ToList();
        }

        public async Task<Subcategory> GetSubcategoryById(Subcategory subcategory)
        {
            return await _db.Subcategories.FirstOrDefaultAsync(x => x.SubCategoryId == subcategory.SubCategoryId);
        }

        public async Task<Subcategory> UpdateSubcategory(Subcategory subcategory)
        {
            var SubcategoryInDb = await _db.Subcategories.FirstOrDefaultAsync(x => x.SubCategoryId == subcategory.SubCategoryId);

            if (SubcategoryInDb == null)
            {
                throw new NullReferenceException("Subcategory does not exist");
            }

            SubcategoryInDb.Name = subcategory.Name;

            _db.Subcategories.Update(SubcategoryInDb);
            await Save();

            return SubcategoryInDb;

        }

        public async Task<Subcategory> DeleteSubcategory(long id)
        {
            var subcategoryToDelete = await _db.Subcategories.FirstOrDefaultAsync(x => x.SubCategoryId == id);
            if(subcategoryToDelete == null)
            {
                throw new NullReferenceException("Subcategory does not exist");
            }

            _db.Subcategories.Remove(subcategoryToDelete);
            await Save();

            return subcategoryToDelete;
        }

        private async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}