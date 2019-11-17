using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
		IList<Item> Items = new List<Item>{
				new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
				new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
				new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
				new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
				new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
				new Item
				{
					Name = "Backstage passes to a TAFKAL80ETC concert",
					SellIn = 15,
					Quality = 20
				},
				new Item
				{
					Name = "Backstage passes to a TAFKAL80ETC concert",
					SellIn = 10,
					Quality = 49
				},
				new Item
				{
					Name = "Backstage passes to a TAFKAL80ETC concert",
					SellIn = 5,
					Quality = 49
				},
				// this conjured item does not work properly yet
				new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
			};

		[Test]
		[TestCase("Aged Brie", 3, 1)]
		[TestCase("Sulfuras", 3, 0)]
		[TestCase("Conjured", 3, -2)]
		[TestCase("Conjured", 0, -4)]
		[TestCase("Backstage passes", 13, 1)]
		[TestCase("Backstage passes", 8, 2)]
		[TestCase("Backstage passes", 3, 3)]
		[TestCase("+5 Dexterity Vest", 3, -1)]
		[TestCase("+5 Dexterity Vest", 0, -2)]
		public void GetQualityChangeOfValue_DifferentParameters_ReturnGoodResult(string itemName, int itemSellIn, int expectedChangeOfValueResult)
		{
			GildedRose gildeRose = new GildedRose(Items);
			int result = gildeRose.GetQualityChangeOfValue(itemName, itemSellIn);
			Assert.AreEqual(expectedChangeOfValueResult, result, $"GetQualityChangeOfValue(): expected {expectedChangeOfValueResult}; got {result}; parameters ( itemName {itemName}; itemSellIn {itemSellIn} )");
		}

		[Test]
		[TestCase("Aged Brie", 3, 2)]
		[TestCase("Sulfuras", 3, 3)]
		[TestCase("Conjured", 0, 0)]
		public void CalculateNewSellIn_DifferentParameters_ReturnGoodResult(string itemName, int itemSellIn, int expectedNewSellInResult)
		{
			GildedRose gildeRose = new GildedRose(Items);
			int result = gildeRose.CalculateNewSellIn(itemName, itemSellIn);
			Assert.AreEqual(expectedNewSellInResult, result, $"CalculateNewSellIn(): expected {expectedNewSellInResult}; got {result}; parameters ( itemName {itemName}; itemSellIn {itemSellIn} )");
		}

		[Test]
		[TestCase("Sulfuras", "Sulfuras", true)]
		[TestCase("Sulfuras", "Conjured", false)]
		public void IterateForSpecialItemsReturnTrueFalse_DifferentParameters_ReturnGoodResult(string listItem, string itemName, bool expectedResult)
		{
			List<string> list = new List<string>() { listItem };
			GildedRose gildeRose = new GildedRose(Items);
			bool result = gildeRose.IterateForSpecialItemsReturnTrueFalse(list, itemName);
			Assert.AreEqual(expectedResult, result, $"IterateForSpecialItemsReturnTrueFalse(): expected {expectedResult}; got {result}; parameters ( itemName {itemName}; list {listItem} )");
		}

		[Test]
		[TestCase("Aged Brie", 30, 50, 50)]
		[TestCase("Sulfuras", 3, 80, 80)]
		[TestCase("Sulfuras", 0, 80, 80)]
		[TestCase("Backstage passes", 0, 30, 0)]
		[TestCase("+5 Dexterity Vest", 3, 4, 3)]
		[TestCase("+5 Dexterity Vest", 0, 4, 2)]
		[TestCase("+5 Dexterity Vest", 0, 0, 0)]
		public void CalculateNewQualityValue_DifferentParameters_ReturnGoodResult(string itemName, int itemSellIn, int itemQuality, int expectedItemQualityResult)
		{
			GildedRose gildeRose = new GildedRose(Items);
			int result = gildeRose.CalculateNewQualityValue(itemName, itemQuality, itemSellIn);
			Assert.AreEqual(expectedItemQualityResult, result, $"CalculateNewQualityValue(): expected {expectedItemQualityResult}; got {result}; parameters ( itemName {itemName}; itemQuality {itemQuality}; itemSellIn {itemSellIn} )");
		}
	}
}
