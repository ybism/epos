using System.Collections.Generic;

namespace epos.Models
{
    public class Subcategory
    {

        public Subcategory(string name)
        {
            Name = name;
        }
        public ICollection<Category> Categories { get; set; }

        public ICollection<FoodItem> FoodItems { get; set; } 
        public long SubCategoryId { get; set; }
        public string Name { get; set; }
    }
}