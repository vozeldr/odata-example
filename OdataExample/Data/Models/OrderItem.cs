using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdataExample.Data.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid OrderItemId { get; set; }

        [Required] public Guid OrderId { get; set; }

        [StringLength(255)] [Required] public string Product { get; set; }

        public virtual Order Order { get; protected set; }
    }
}
