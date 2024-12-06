using System;

namespace GroceryStorePOS.Models
{
    public class InventoryItem : IComparable<InventoryItem>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Constructor to initialize the item
        public InventoryItem(int id, string name, decimal price, int quantityInStock)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantityInStock;
        }

        // Check if the item is low in stock
        public bool IsLowInStock(int threshold)
        {
            return Quantity < threshold;
        }

        public override string ToString()
        {
            return $"{Name} (ID: {Id})\nIn stock: {Quantity}\nPrice: {Price:C}";
        }

        // Compare two InventoryItem objects by their Id
        public int CompareTo(InventoryItem other)
        {
            if (other == null) return 1;
            return this.Id.CompareTo(other.Id);
        }
    }
}
