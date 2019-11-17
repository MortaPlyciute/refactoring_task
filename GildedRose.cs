using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
		
		List<string> specialItemsQualityRulePassSellInDrop = new List<string>() { "Backstage passes" };
		List<string> specialItemsQualityRuleMax = new List<string>() { "Sulfuras" };
		List<string> specialItemsSellIn = new List<string>() { "Sulfuras" };

		int maxItemQuality = 50;

		public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
			foreach (Item item in Items)
			{
				item.Quality = CalculateNewQualityValue(item.Name, item.Quality, item.SellIn);
				item.SellIn = GetQualityChangeOfValue(item.Name, item.SellIn);
			}
        }

		public int GetQualityChangeOfValue(string itemName, int itemSellIn)
		{
			if (itemName.Contains("Aged Brie"))
				return 1;
			if (itemName.Contains("Sulfuras"))
				return 0;
			if (itemName.Contains("Conjured") && itemSellIn > 0)
				return -2;
			else if (itemName.Contains("Conjured"))
				return -4;
			if (itemName.Contains("Backstage passes") && itemSellIn > 10)
				return 1;
			else if (itemName.Contains("Backstage passes") && itemSellIn > 5)
				return 2;
			else if (itemName.Contains("Backstage passes"))
				return 3;

			if (itemSellIn > 0)
				return -1;

			return -2;
		}

		public int CalculateNewQualityValue(string itemName, int itemQuality, int itemSellIn)
		{
			int qualityChange = GetQualityChangeOfValue(itemName, itemSellIn);

			if (IterateForSpecialItemsReturnTrueFalse(specialItemsQualityRulePassSellInDrop, itemName) && itemSellIn <= 0)
				return 0;

			itemQuality = itemQuality + qualityChange;

			if (itemQuality > maxItemQuality && !IterateForSpecialItemsReturnTrueFalse(specialItemsQualityRuleMax, itemName))
				itemQuality = maxItemQuality;

			if (itemQuality < 0)
				itemQuality = 0;

			return itemQuality;
		}

		public int CalculateNewSellIn(string itemName, int itemSellIn)
		{
			if (IterateForSpecialItemsReturnTrueFalse(specialItemsSellIn, itemName))
				return itemSellIn;

			return itemSellIn--;
		}
		
		public bool IterateForSpecialItemsReturnTrueFalse (List<string> list, string itemName)
		{
			foreach (string specialNamePart in list)
			{
				if (itemName.Contains(specialNamePart))
					return true;
			}
			return false;
		}
	}
}
