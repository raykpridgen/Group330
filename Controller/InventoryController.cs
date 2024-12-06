using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GroceryStorePOS.Models;
using GroceryStorePOS.Repositories;
using GroceryStorePOS.Services;

namespace GroceryStorePOS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<InventoryItem>> GetAllItems()
        {
            return Ok(_service.GetAllItems());
        }

        [HttpGet("{id}")]
        public ActionResult<InventoryItem> GetItemById(int id)
        {
            var item = _service.GetItemById(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public ActionResult AddItem([FromBody] InventoryItem item)
        {
            _service.AddItem(item);
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(int id, [FromBody] InventoryItem updatedItem)
        {
            var item = _service.GetItemById(id);
            if (item == null)
                return NotFound();

            updatedItem.Id = id;
            _service.UpdateItem(updatedItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveItem(int id)
        {
            var item = _service.GetItemById(id);
            if (item == null)
                return NotFound();

            _service.RemoveItem(id);
            return NoContent();
        }
        
        [HttpGet("lowstock/{threshold}")]
        public ActionResult<List<InventoryItem>> GetLowStockItems(int threshold)
        {
            var lowStockItems = _service.GetLowStockItems(threshold);
            if (lowStockItems == null || lowStockItems.Count == 0)
                return NotFound();

            return Ok(lowStockItems);
        }
    }
}
