using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly InMemItemsRepository _repository;

        public ItemsController()
        {
            _repository = new InMemItemsRepository();
        }

        // GET /Items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = _repository.GetItems();
            return items;
        }

        // GET /Items/{id}
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = _repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            return item;
        }
    }
}