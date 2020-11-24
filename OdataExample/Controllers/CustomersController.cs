using System;
using Microsoft.AspNetCore.Mvc;
using OdataExample.Data;
using OdataExample.Data.Models;

namespace OdataExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class CustomersController : BaseEntityController<Customer, Guid>
    {
        public CustomersController(DataContext context) : base(context)
        {
        }
    }
}
