using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdataExample.Data.Models
{
    public class Customer
    {
        private ICollection<Order> _orders;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CustomerId { get; set; }

        [StringLength(255)] [Required] public string Name { get; set; }

        public virtual ICollection<Order> Orders
        {
            get => _orders ??= new List<Order>();
            set => _orders = value;
        }
    }
}
