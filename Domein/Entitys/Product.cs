﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Entitys
{
    public class Product
    { 
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public decimal RegularPrice { get; set; } 
        public decimal? Discount { get; set; }
        public decimal? DiscountedPrice {  get; set; }
        public string UnitType { get; set; } = "Count";
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public byte[]? Image { get; set; }
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; } = null!;
    }
}
