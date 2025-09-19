namespace GildedRose.Core
{
    /// <summary>
    /// Represents an item of goods being sold at the shop.
    /// Note: As per the instructions, do not modify this class or its properties.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of days remaining to sell the item.
        /// Think of it like days before the expiry date.
        /// </summary>
        public int SellIn { get; set; }

        /// <summary>
        /// Quality denotes how valuable the item is.
        /// Higher the quality, more valuable the item.
        /// </summary>
        public int Quality { get; set; }
    }
}
