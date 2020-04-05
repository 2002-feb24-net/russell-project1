﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int Id { get; set; }

        [Required]
        [DisplayName("Location Id")]
        public int LocationId { get; set; }

        [Required]
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Locations Location { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
