using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OdataExample.Data;

namespace OdataExample.Controllers
{
    public abstract class BaseEntityController<TEntity, TEntityId> : ODataController
        where TEntity : class
        where TEntityId : IComparable, IComparable<TEntityId>, IEquatable<TEntityId>
    {
        private readonly DataContext _context;

        protected BaseEntityController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(200)]
        public IQueryable<TEntity> Get()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TEntity>> Get(TEntityId id)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }
    }
}
