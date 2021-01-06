using System;
using System.Collections.Generic;

namespace epos.Models
{
    public class Order
    {
        public Order(User user, List<FoodItem> foodItem)
        {
            User = user;
            FoodItem = foodItem;
            TimeOfOrder = DateTime.Now;
        }

        public int Id { get; set; }
        public User User;
        public ICollection<FoodItem> FoodItem { get; set; }
        public DateTime TimeOfOrder;
        public long TotalPrice { get; set; }

    }
}