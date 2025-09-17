namespace GildedRose.Console
{
    // Structure unchanged because properties "belong to the goblin in the corner who will
    // insta-rage" me otherwise. ;)
    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

    // Note for future - I'd probably add a nice builder here and lock down setters.
}
