using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flower.Models
{
    [Table("Purchase")]
    public class Purchase
    {
        public int PurchaseID { get; set; }

        [Display(Name = "Seller")]
        [Required(ErrorMessage = "Choose Seller")]
        public int SellerID { get; set; }

        [Display(Name = "Customer")]
        [Required(ErrorMessage = "Choose Customer")]
        public int CustomerID { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Choose Product")]
        public int ProductID { get; set; }

        [Display(Name = "Purchase Date")]
        [Required(ErrorMessage = "Enter Date")]
        public DateTime PurchaseDate { get; set; }

        public Seller Seller { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}
