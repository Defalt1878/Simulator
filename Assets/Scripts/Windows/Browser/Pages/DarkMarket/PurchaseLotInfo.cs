using System;

namespace Windows.Browser.Pages.DarkMarket
{
	[Serializable]
	public class PurchaseLotInfo
	{
		public string Name { get; private set; }
		public string Info { get; private set; }
		public float Price { get; private set; }
		public float HashRate { get; private set; }
		public string Location { get; private set; }

		public PurchaseLotInfo(string name, string location, float hashRate, float price)
		{
			Name = name;
			HashRate = hashRate;
			Location = location;
			Info = $"Location : {location}.\nHashRate: {hashRate:0.##}";
			Price = price;
		}
	}
}