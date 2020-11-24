using System;
using Microsoft.AspNetCore.Mvc;
using OdataExample.Data;
using OdataExample.Data.Models;

namespace OdataExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class OrdersController : BaseEntityController<Order, Guid>
    {
        public OrdersController(DataContext context) :
            base(context)
        {
        }
    }
}
