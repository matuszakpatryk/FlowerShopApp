using Flower.Extensions.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flower.Models
{
    [Table("Seller")]
    public class Seller
    {
        [Key]
        public int SellerID { get; set; }

        [Capitalized]
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Capitalized]
        [Required(ErrorMessage = "Enter Surname")]
        public string Surname { get; set; }

        [EmployeeNumberCheck]
        [Required(ErrorMessage = "Enter Employee Number")]
        public string EmployeeNumber { get; set; }

        [PhoneCheck]
        [Required(ErrorMessage = "Enter Phone")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Enter Country Name")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Enter City Name")]
        public string City { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }

    }
}
