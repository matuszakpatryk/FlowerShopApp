using Flower.Extensions.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flower.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [Display(Name = "ID")]
        public int CustomerID { get; set; }

        [Capitalized]
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Capitalized]
        [Required(ErrorMessage = "Enter Surname")]
        public string Surname { get; set; }

        [PhoneCheck]
        [Required(ErrorMessage = "Enter Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Country Name")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Enter City Name")]
        public string City { get; set; }

        [Display(Name = "Purchases")]
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
