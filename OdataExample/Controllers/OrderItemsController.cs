using System;
using Microsoft.AspNetCore.Mvc;
using OdataExample.Data;
using OdataExample.Data.Models;

namespace OdataExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class OrderItemsController : BaseEntityController<OrderItem, Guid>
    {
        public OrderItemsController(DataContext context) :
            base(context)
        {
        }
    }
}
