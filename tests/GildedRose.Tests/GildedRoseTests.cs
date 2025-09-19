using Xunit;
using System.Collections.Generic;
using GildedRose.Core;

namespace GildedRose.Tests
{
    public class GildedRoseTests
    {
        private static GildedRoseApp CreateApp(params Item[] items) =>
            new GildedRoseApp(new List<Item>(items));

        // --- Normal Items ---
        [Fact]
        public void NormalItem_DecreasesInQualityAndSellIn()
        {
            var app = CreateApp(new Item { Name = "Normal Item", SellIn = 10, Quality = 20 });

            app.UpdateQuality();

            Assert.Equal(9, app.Items[0].SellIn);
            Assert.Equal(19, app.Items[0].Quality);
        }

        [Fact]
        public void NormalItem_QualityDegradesTwiceAsFast_AfterSellDate()
        {
            var app = CreateApp(new Item { Name = "Normal Item", SellIn = 0, Quality = 20 });

            app.UpdateQuality();

            Assert.Equal(-1, app.Items[0].SellIn);
            Assert.Equal(18, app.Items[0].Quality);
        }

        [Fact]
        public void NormalItem_QualityNeverNegative()
        {
            var app = CreateApp(new Item { Name = "Normal Item", SellIn = 5, Quality = 0 });

            app.UpdateQuality();

            Assert.Equal(0, app.Items[0].Quality);
        }

        // --- Aged Brie ---
        [Fact]
        public void AgedBrie_IncreasesInQuality()
        {
            var app = CreateApp(new Item { Name = "Aged Brie", SellIn = 10, Quality = 20 });

            app.UpdateQuality();

            Assert.Equal(21, app.Items[0].Quality);
        }

        [Fact]
        public void AgedBrie_QualityNeverExceeds50()
        {
            var app = CreateApp(new Item { Name = "Aged Brie", SellIn = 5, Quality = 50 });

            app.UpdateQuality();

            Assert.Equal(50, app.Items[0].Quality);
        }

        [Fact]
        public void AgedBrie_IncreasesEvenAfterSellInDate()
        {
            var app = CreateApp(new Item { Name = "Aged Brie", SellIn = 0, Quality = 48 });

            app.UpdateQuality();

            Assert.Equal(50, app.Items[0].Quality);  // matches current legacy behaviour
        }

        // --- Sulfuras ---
        [Fact]
        public void Sulfuras_NeverChanges()
        {
            var app = CreateApp(new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 });

            app.UpdateQuality();

            Assert.Equal(10, app.Items[0].SellIn);
            Assert.Equal(80, app.Items[0].Quality);
        }

        // --- Backstage Passes ---
        [Fact]
        public void BackstagePasses_IncreaseNormally_WhenMoreThan10Days()
        {
            var app = CreateApp(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 });

            app.UpdateQuality();

            Assert.Equal(21, app.Items[0].Quality);
        }

        [Fact]
        public void BackstagePasses_IncreaseBy2_When10DaysOrLess()
        {
            var app = CreateApp(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 });

            app.UpdateQuality();

            Assert.Equal(22, app.Items[0].Quality);
        }

        [Fact]
        public void BackstagePasses_IncreaseBy3_When5DaysOrLess()
        {
            var app = CreateApp(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20 });

            app.UpdateQuality();

            Assert.Equal(23, app.Items[0].Quality);
        }

        [Fact]
        public void BackstagePasses_DropToZero_AfterConcert()
        {
            var app = CreateApp(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 });

            app.UpdateQuality();

            Assert.Equal(0, app.Items[0].Quality);
        }

        [Fact]
        public void BackstagePasses_CannotExceed50()
        {
            var app = CreateApp(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 49 });

            app.UpdateQuality();

            Assert.Equal(50, app.Items[0].Quality);
        }

        // --- Conjured (skipped until implemented) ---
        [Fact(Skip = "Not implemented yet")]
        public void Conjured_DegradesTwiceAsFast()
        {
            var app = CreateApp(new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 });

            app.UpdateQuality();

            Assert.Equal(4, app.Items[0].Quality); // expectation once implemented
        }
    }
}
