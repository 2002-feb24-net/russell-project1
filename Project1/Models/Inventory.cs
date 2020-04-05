using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public partial class Inventory
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Location Id")]
        public int LocationId { get; set; }

        [Required]
        [DisplayName("Product Id")]
        public int ProductId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public virtual Locations Location { get; set; }
        public virtual Products Product { get; set; }
    }
}
