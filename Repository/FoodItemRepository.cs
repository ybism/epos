using epos.Models;
using epos.Repository.IRepository;
using epos.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace epos.Repository
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly FoodItemContext _db;

        public FoodItemRepository(FoodItemContext db)
        {
            _db = db;
        }

        //Add item to db
        public async Task<FoodItem> addItem(FoodItem item)
        {
            await _db.AddAsync(item);
            await Save();

            return item;
        }

        //Return all items in db
        public List<FoodItem> getItems()
        {
            return _db.FoodItems.ToList();
        }

        //Get item by ID
        public async Task<FoodItem> getItemById(long Id)
        {
            return await _db.FoodItems.FirstOrDefaultAsync(x => x.Id == Id);
        }

        //Update item details in db
        public async Task<FoodItem> updateItem(FoodItem item)
        {
            var originalItem = await _db.FoodItems.FirstOrDefaultAsync(x => x.Id == item.Id);

            if (originalItem == null)
            {
                throw new NullReferenceException("Item does not exist");
            }

            originalItem.Id = item.Id;
            originalItem.Name = item.Name;
            originalItem.Size = item.Size;

            _db.Update(item);
            await Save();
            return item;

        }

        public async Task removeItem(long id)
        {
            var itemToBeDeleted = await _db.FoodItems.FirstOrDefaultAsync(x => x.Id == id);

            if(itemToBeDeleted == null)
            {
                throw new NullReferenceException("item doesnt exist");
            }

            _db.FoodItems.Remove(itemToBeDeleted);
            await Save();
        }

        private async Task Save()
        {
            await _db.SaveChangesAsync();
        }

    }
}