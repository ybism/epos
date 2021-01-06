using System.Collections.Generic;


namespace epos.Models
{
    public class Menu
    {
    
        public Menu(string name)
        {
            Name = Name;
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
