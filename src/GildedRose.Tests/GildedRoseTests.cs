using Xunit;
using System.Collections.Generic;
using GildedRose.Console;

namespace GildedRose.Tests
{
    public class GildedRoseTests
    {
        [Fact]
        public void UpdateQuality_NormalItem_BeforeSellByDate()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Normal Item", SellIn = 10, Quality = 20 } };
            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(9, items[0].SellIn);
            Assert.Equal(19, items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_NormalItem_AfterSellByDate()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Normal Item", SellIn = 0, Quality = 20 } };
            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(-1, items[0].SellIn);
            Assert.Equal(18, items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_QualityNeverNegative()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Normal Item", SellIn = 10, Quality = 0 } };
            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(0, items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_AgedBrie_IncreasesInQuality()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 20 } };
            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(9, items[0].SellIn);
            Assert.Equal(21, items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_QualityNeverMoreThan50()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 50 } };
            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(50, items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_Sulfuras_NeverChanges()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 } };
            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(10, items[0].SellIn);
            Assert.Equal(80, items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_BackstagePasses_IncreasesInQuality_MoreThan10Days()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } };
            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(14, items[0].SellIn);
            Assert.Equal(21, items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_BackstagePasses_IncreasesInQuality_10DaysOrLess()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 } };
            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(9, items[0].SellIn);
            Assert.Equal(22, items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_BackstagePasses_IncreasesInQuality_5DaysOrLess()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20 } };
            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(4, items[0].SellIn);
            Assert.Equal(23, items[0].Quality);
        }

        [Fact]
        public void UpdateQuality_BackstagePasses_DropsToZero_AfterConcert()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 } };
            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(0, items[0].Quality);
        }

        [Fact(Skip = "Not implemented yet")]
        public void UpdateQuality_Conjured_DegradesTwiceAsFast()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 20 } };
            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(9, items[0].SellIn);
            Assert.Equal(18, items[0].Quality);
        }

        [Fact(Skip = "Not implemented yet")]
        public void UpdateQuality_Conjured_DegradesTwiceAsFast_AfterSellByDate()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 20 } };
            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(-1, items[0].SellIn);
            Assert.Equal(16, items[0].Quality);
        }
    }
}
