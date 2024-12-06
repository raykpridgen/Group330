using System.Collections.Generic;
using GroceryStorePOS;
using GroceryStorePOS.Models;

namespace GroceryStorePOS.Repositories
{
    public interface IInventoryRepository
    {
        List<InventoryItem> GetAllItems();
        InventoryItem GetItemById(int id);
        void AddItem(InventoryItem item);
        void UpdateItem(InventoryItem item);
        void RemoveItem(int id);
        public List<InventoryItem> GetLowStockItems(int threshold);

    }
}
