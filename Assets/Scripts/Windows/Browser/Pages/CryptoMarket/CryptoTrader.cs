using System;
using UserData;

namespace Windows.Browser.Pages.CryptoMarket
{
	public class CryptoTrader
	{
		private readonly Crypto _crypto;

		public CryptoTrader(Crypto crypto)
		{
			_crypto = crypto;
		}

		public string TryTradeCrypto(float tradeAmount, CryptoPageState tradeState)
		{
			return tradeState switch
			{
				CryptoPageState.Buy => TryBuy(tradeAmount),
				CryptoPageState.Sell => TrySell(tradeAmount),
				_ => throw new NotSupportedException()
			};
		}

		private string TryBuy(float buyAmount)
		{
			var instance = StaticData.GetInstance();
			var stats = instance.Stats;
			var moneyNeeded = Crypto.ToMoney(_crypto, buyAmount);
			if (stats.Money.Value < moneyNeeded)
				return "Not enough money!";

			stats.Money.Value -= moneyNeeded;
			_crypto.Value += buyAmount;
			return null;
		}

		private string TrySell(float sellAmount)
		{
			if (_crypto.Value < sellAmount)
				return "Not enough crypto!";

			var instance = StaticData.GetInstance();
			var stats = instance.Stats;
			stats.Money.Value += Crypto.ToMoney(_crypto, sellAmount);
			_crypto.Value -= sellAmount;
			return null;
		}
	}
}