using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _repository;

        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }

        // GET /Items
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await _repository.GetItemsAsync())
                        .Select(item => item.AsDto());
            return items;
        }

        // GET /Items/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await _repository.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto createItemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await _repository.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = updateItemDto.Name,
                Price = updateItemDto.Price
            };
            await _repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await _repository.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            await _repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}