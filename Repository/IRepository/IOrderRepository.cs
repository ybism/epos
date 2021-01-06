using epos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace epos.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(int userId, List<FoodItem> order);
        Task<Order> UpdateOrder(Order order);
        Task DeleteOrder(long id);
        ICollection<Order> GetOrders();
        Task<Order> GetOrderById(int Id);
    }
}