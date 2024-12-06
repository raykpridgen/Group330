using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GroceryStorePOS.Models;

namespace GroceryStorePOS.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private MySqlConnection _connection;

        // Constructor to initialize the database connection
        public InventoryRepository()
        {
            string connectionString = "server=localhost;userid=csci330user;password=csco300pass;database=grocerystorepos";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }

        // Destructor to ensure the connection is closed when the object is destroyed
        ~InventoryRepository()
        {
            _connection.Close();
        }

        // Get all items from the database
        public List<InventoryItem> GetAllItems()
        {
            var statement = "SELECT * FROM InventoryItems";
            var command = new MySqlCommand(statement, _connection);
            var results = command.ExecuteReader();

            List<InventoryItem> items = new List<InventoryItem>(20);

            while (results.Read())
            {
                InventoryItem item = new InventoryItem((int)results["Id"], (string)results["Name"], (decimal)results["Price"], (int)results["Quantity"]);
                items.Add(item);
            }

            results.Close();
            return items;
        }

        // Get a specific inventory item by its Id
        public InventoryItem GetItemById(int id)
        {
            var statement = "SELECT * FROM InventoryItems WHERE Id = @id";
            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@id", id);

            var results = command.ExecuteReader();
            InventoryItem item = null;

            if (results.Read())
            {
                item = new InventoryItem((int)results["Id"], (string)results["Name"], (decimal)results["Price"], (int)results["Quantity"]);

            }

            results.Close();
            return item;
        }

        // Add a new inventory item to the database
        public void AddItem(InventoryItem item)
        {
            var statement = "INSERT INTO InventoryItems (Name, Quantity, Price) VALUES (@name, @quantity, @price)";
            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@name", item.Name);
            command.Parameters.AddWithValue("@quantity", item.Quantity);
            command.Parameters.AddWithValue("@price", item.Price);

            command.ExecuteNonQuery();
        }

        // Update an existing inventory item
        public void UpdateItem(InventoryItem item)
        {
            var statement = "UPDATE InventoryItems SET Name = @name, Quantity = @quantity, Price = @price WHERE Id = @id";
            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@name", item.Name);
            command.Parameters.AddWithValue("@quantity", item.Quantity);
            command.Parameters.AddWithValue("@price", item.Price);
            command.Parameters.AddWithValue("@id", item.Id);

            command.ExecuteNonQuery();
        }

        // Delete an inventory item by its Id
        public void RemoveItem(int id)
        {
            var statement = "DELETE FROM InventoryItems WHERE Id = @id";
            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
        public List<InventoryItem> GetLowStockItems(int threshold)
        {
            var items = new List<InventoryItem>();

            string query = "SELECT Id, Name, Price, Quantity FROM InventoryItems WHERE Quantity < @Threshold";
            
            using (var command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Threshold", threshold);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new InventoryItem(
                            reader.GetInt32("Id"),
                            reader.GetString("Name"),
                            reader.GetDecimal("Price"),
                            reader.GetInt32("Quantity")
                        );
                        items.Add(item);
                    }
                }
            }

            return items;
        }
    }
}
