using System;

namespace Windows.Browser.Pages.DarkMarket
{
	[Serializable]
	public class PurchaseLotInfo
	{
		public string Name { get; }
		public string Info { get; }
		public float Price { get; }
		public float HashRate { get; }
		public string Location { get; }

		public PurchaseLotInfo(string name, string location, float hashRate, float price)
		{
			Name = name;
			Location = location;
			Info = $"Location : {location}.\nHashRate: {hashRate:0.##}";
			HashRate = hashRate;
			Price = price;
		}
	}
}