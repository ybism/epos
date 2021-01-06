using epos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace epos.Repository.IRepository
{
    public interface IFoodItemRepository
    {
        Task removeItem(long id);
        Task<FoodItem> addItem(FoodItem item);
        List<FoodItem> getItems();
        Task<FoodItem> updateItem(FoodItem item);
        Task<FoodItem> getItemById(long id);

    }
}