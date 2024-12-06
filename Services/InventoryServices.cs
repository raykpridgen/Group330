using System.Collections.Generic;
using GroceryStorePOS;
using GroceryStorePOS.Repositories;
using GroceryStorePOS.Models;

namespace GroceryStorePOS.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<InventoryItem> GetAllItems()
        {
            return _repository.GetAllItems();
        }

        public InventoryItem GetItemById(int id)
        {
            return _repository.GetItemById(id);
        }

        public void AddItem(InventoryItem item)
        {
            _repository.AddItem(item);
        }

        public void UpdateItem(InventoryItem item)
        {
            _repository.UpdateItem(item);
        }

        public void RemoveItem(int id)
        {
            _repository.RemoveItem(id);
        }
        public List<InventoryItem> GetLowStockItems(int threshold)
        {
            return _repository.GetLowStockItems(threshold);
        }
    }
}
