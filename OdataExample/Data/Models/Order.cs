using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdataExample.Data.Models
{
    public class Order
    {
        private ICollection<OrderItem> _orderItems;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid OrderId { get; set; }

        [Required] public Guid CustomerId { get; set; }

        [StringLength(255)] [Required] public string Name { get; set; }

        public virtual Customer Customer { get; protected set; }

        public virtual ICollection<OrderItem> OrderItems
        {
            get => _orderItems ??= new List<OrderItem>();
            set => _orderItems = value;
        }
    }
}
