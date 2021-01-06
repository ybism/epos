using System.Collections.Generic;
using epos.Models;
using epos.Repository.IRepository;
using epos.DAL;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace epos.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FoodItemContext _db;

        public OrderRepository(FoodItemContext db)
        {
            _db = db;
        }
        public async Task<Order> CreateOrder(int userId, List<FoodItem> order)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
            {
                throw new NullReferenceException("this user does not exist");
            }

            var Order = new Order(user, order);

            _db.Orders.Add(Order);
            await Save();

            return Order;

        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _db.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public ICollection<Order> GetOrders()
        {
            return _db.Orders.ToList();
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var orderInDb = await _db.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);

            if (orderInDb == null)
            {
                throw new NullReferenceException("Order does not exist");
            }

            orderInDb.FoodItem = order.FoodItem;
            orderInDb.TotalPrice = order.TotalPrice;

            _db.Orders.Update(orderInDb);
            await Save();

            return orderInDb;
        }

        public async Task DeleteOrder(long id)
        {
            var orderToBeDeleted = await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (orderToBeDeleted == null)
            {
                throw new NullReferenceException("order does not exist");
            }
            _db.Orders.Remove(orderToBeDeleted);
            await Save();
        }

        private async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}