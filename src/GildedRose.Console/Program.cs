using System.Collections.Generic;
using GildedRose.Core;

namespace GildedRose.Console
{
    public class Program
    {
        private readonly Inventory _inventory;

        /// <summary>
        /// Initializes the program / app with default inventory items.
        /// </summary>
        public Program() : this(new Inventory(GetDefaultItems())) { }

        /// <summary>
        /// Initializes the program with the given inventory.
        /// </summary>
        /// <param name="inventory">Current product inventory.</param>
        public Program(Inventory inventory)
        {
            _inventory = inventory;
        }

        /// <summary>
        /// Updates the quality and days-to-expiry (SellIn) values of the products in the inventory.
        /// </summary>
        /// <seealso cref="Inventory.UpdateStock(int)"/>
        // Note: Keeping `UpdateQuality()` method signature untouched as per the requirements.
        public void UpdateQuality() => _inventory.UpdateStock();

        /// <summary>
        /// Prints the current inventory items to the console.
        /// </summary>
        /// <seealso cref="Inventory.ToString"/>"/>
        public void PrintCurrentInventory() => System.Console.WriteLine(_inventory);

        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to GildedRose Inventory Management System.");

            System.Console.WriteLine("Initializing inventory with default items...");
            var program = new Program();

            System.Console.WriteLine("Current Inventory:");
            program.PrintCurrentInventory();

            System.Console.WriteLine("Updating inventory for one day...");
            program.UpdateQuality();

            System.Console.WriteLine("Inventory after one day:");
            program.PrintCurrentInventory();

            System.Console.WriteLine("Please press any key to exit.");
            System.Console.ReadKey();
        }

        /// <summary>
        /// Helper method to get default inventory items for initializing stock.
        /// </summary>
        /// <returns>List of default inventory items.</returns>
        // TODO - Consider refactoring this so that the items are not hard-coded.
        private static IReadOnlyList<Item> GetDefaultItems() => new List<Item>
        {
            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
            new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
            new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
        };
    }
}
