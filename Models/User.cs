using System.Collections.Generic;

namespace epos.Models
{
    public class User
    {
        public User(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public long UserId { get; set; }
        public string Name;
        public string Address;
        public List<Order> Orders {get; set;}
    }
}