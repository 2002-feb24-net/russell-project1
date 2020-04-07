using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }

        [DisplayName("Customer Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Card Number")]
        [Range(0, int.MaxValue)]
        public int? CardNumber { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
