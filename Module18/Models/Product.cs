using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Module18.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; } = null!;

        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [StringLength(20)]
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }

        [Range(0, 32767)]
        public short? UnitsInStock { get; set; }
        [Range(0, 32767)]
        public short? UnitsOnOrder { get; set; }
        [Range(0, 32767)]
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
    }
}
