using System.Collections.Generic;

namespace epos.Models
{
    public class FoodItem
    {

        public FoodItem(string name, string size, long price)
        {
            Name = name;
            Size = size;
            Price = price;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public long Price { get; set; }
        public ICollection<Subcategory> Subcateories { get; set; }
    }
}