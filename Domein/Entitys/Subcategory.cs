using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Entitys
{
    public class Subcategory
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty; 
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
