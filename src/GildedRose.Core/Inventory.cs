using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Core
{
    /// <summary>
    /// Represents the product inventory and contains methods to update stock (expiry and quality values).
    /// </summary>
    public class Inventory
    {
        private const string AgedBrie = "Aged Brie";
        private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const int MaxQuality = 50;

        /// <summary>
        /// Inventory items.
        /// </summary>
        public IReadOnlyList<Item> Items { get; }

        /// <summary>
        /// Initializes a new inventory with given items.
        /// </summary>
        /// <param name="items">Stock items to initialize the inventory with.</param>
        public Inventory(IReadOnlyList<Item> items) { Items = items; }

        /// <summary>
        /// Updates the stock by adjusting the <code>Quality</code> and the <code>SellIn</code> values of each item.
        /// </summary>
        /// <param name="numberOfDays">The number of days to advance the stock forward. Default: 1 day.</param>
        public void UpdateStock(int numberOfDays = 1)
        {
            foreach (var item in Items)
            {
                for (int i = 0; i < numberOfDays; i++)
                {
                    DecrementSellIn(item);
                    UpdateItemQuality(item);
                }
            }
        }

        /// <summary>
        /// Decrements <code>SellIn</code> value of an applicable item (excluding legendary items)
        /// </summary>
        private static void DecrementSellIn(Item item)
        {
            if (!IsLegendary(item))
            {
                item.SellIn--;
            }
        }

        /// <summary>
        /// Adjusts item quality for one day, following its specific business rules.
        /// </summary>
        private static void UpdateItemQuality(Item item)
        {
            // No quality updates for legendary items
            if (IsLegendary(item)) { return; }

            // Item-specific and standard quality updates
            switch (item.Name)
            {
                case AgedBrie:
                    UpdateAgedBrie(item);
                    break;
                case BackstagePasses:
                    UpdateBackstagePasses(item);
                    break;
                default:
                    UpdateStandardItem(item);
                    break;
            }
        }

        /// <summary>
        /// Determines whether the specified item is a legendary item (i.e. "Sulfuras.")
        /// Legendary items never have to be sold or decrease in Quality.
        /// </summary>
        // TODO - Check if this needs to be an exact match or culture-respecting
        private static bool IsLegendary(Item item) => item.Name == Sulfuras;

        private static void UpdateAgedBrie(Item item)
        {
            if (item.Quality < MaxQuality)
            {
                item.Quality++;
                if (item.SellIn < 0 && item.Quality < MaxQuality)
                {
                    item.Quality++;
                }
            }
        }

        private static void UpdateBackstagePasses(Item item)
        {
            if (item.SellIn < 0)
            {
                item.Quality = 0;
                return;
            }

            if (item.Quality < MaxQuality)
            {
                item.Quality++;

                if (item.SellIn < 10 && item.Quality < MaxQuality)
                {
                    item.Quality++;
                }

                if (item.SellIn < 5 && item.Quality < MaxQuality)
                {
                    item.Quality++;
                }
            }
        }

        private static void UpdateStandardItem(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality--;
                if (item.SellIn < 0 && item.Quality > 0)
                {
                    item.Quality--;
                }
            }
        }

        /// <summary>
        /// Returns a string representation of the current inventory, listing all items with their SellIn and Quality values.
        /// </summary>
        public override string ToString() =>
            "============================== Inventory ==============================" +
            Environment.NewLine +
            string.Join(Environment.NewLine, Items.Select(i => $"{i.Name}, SellIn: {i.SellIn}, Quality: {i.Quality}")) +
            Environment.NewLine +
            "=======================================================================";
    }
}
