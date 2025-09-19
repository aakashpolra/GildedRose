using System.Collections.Generic;

namespace GildedRose.Core
{
    public class GildedRoseApp
    {
        private const string AgedBrie = "Aged Brie";
        private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const int MaxQuality = 50;

        public IReadOnlyList<Item> Items { get; private set; }

        public GildedRoseApp(IReadOnlyList<Item> items) { Items = items; }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdateItemQuality(item);
            }
        }

        private static void UpdateItemQuality(Item item)
        {
            if (item.Name == Sulfuras)
                return;

            item.SellIn--;

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
    }
}
