using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public partial class OrderItems
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Order Id")]
        public int OrderId { get; set; }

        [Required]
        [DisplayName("Product Id")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
