using System.Collections.Generic;

namespace epos.Models
{
    public class Category
    {
        public Category(string name)
        {
            Name = name;
        }

        public ICollection<Subcategory> Subcategories { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Menu> Menus { get; set; }
    }
}