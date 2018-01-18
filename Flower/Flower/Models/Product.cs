using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flower.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Enter Quantity")]
        public int Quantity { get; set; }
    }
}
