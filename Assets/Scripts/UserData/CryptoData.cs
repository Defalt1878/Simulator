using System;

namespace UserData
{
	[Serializable]
	public class CryptoData
	{
		public Crypto Bitcoin { get; }
		public Crypto Ethereum { get; }
		public Crypto Tether { get; }
		public Crypto Dogecoin { get; }

		public CryptoData()
		{
			Bitcoin = new Crypto(nameof(Bitcoin), 26000, 36000);
			Ethereum = new Crypto(nameof(Ethereum), 1800, 2400);
			Tether = new Crypto(nameof(Tether), 0.6f, 1.4f);
			Dogecoin = new Crypto(nameof(Dogecoin), 0.004f, 0.02f);
		}

		// 	private float _bitcoin;
		// private float _ethereum;
		// private float _tether;
		// private float _dogecoin;
		//
		// [Currency(nameof(BitcoinExchangeRate))]
		// public float Bitcoin
		// {
		// 	get => _bitcoin;
		// 	set
		// 	{
		// 		if (value < 0)
		// 			throw new ArgumentException();
		// 		_bitcoin = value;
		// 		OnValueChanged?.Invoke(nameof(Bitcoin), Bitcoin);
		// 	}
		// }
		//
		// [Currency(nameof(EthereumExchangeRate))]
		// public float Ethereum
		// {
		// 	get => _ethereum;
		// 	set
		// 	{
		// 		if (value < 0)
		// 			throw new ArgumentException();
		// 		_ethereum = value;
		// 		OnValueChanged?.Invoke(nameof(Ethereum), Ethereum);
		// 	}
		// }
		//
		// [Currency(nameof(TetherExchangeRate))]
		// public float Tether
		// {
		// 	get => _tether;
		// 	set
		// 	{
		// 		if (value < 0)
		// 			throw new ArgumentException();
		// 		_tether = value;
		// 		OnValueChanged?.Invoke(nameof(Tether), Tether);
		// 	}
		// }
		//
		// [Currency(nameof(DogecoinExchangeRate))]
		// public float Dogecoin
		// {
		// 	get => _dogecoin;
		// 	set
		// 	{
		// 		if (value < 0)
		// 			throw new ArgumentException();
		// 		_dogecoin = value;
		// 		OnValueChanged?.Invoke(nameof(Dogecoin), Dogecoin);
		// 	}
		// }
		//
		// private float _bitcoinExchangeRate = 30296.90f;
		// private float _ethereumExchangeRate = 2040.40f;
		// private float _tetherExchangeRate = 1.00f;
		// private float _dogecoinExchangeRate = 0.087f;
		//
		// [ExchangeRate(26000, 36000)]
		// public float BitcoinExchangeRate
		// {
		// 	get => _bitcoinExchangeRate;
		// 	set
		// 	{
		// 		if (value < 0)
		// 			throw new ArgumentException();
		// 		_bitcoinExchangeRate = value;
		// 		OnRateChanged?.Invoke(nameof(Bitcoin), BitcoinExchangeRate);
		// 	}
		// }
		//
		// [ExchangeRate(1800, 2400)]
		// public float EthereumExchangeRate
		// {
		// 	get => _ethereumExchangeRate;
		// 	set
		// 	{
		// 		if (value < 0)
		// 			throw new ArgumentException();
		// 		_ethereumExchangeRate = value;
		// 		OnRateChanged?.Invoke(nameof(Ethereum), EthereumExchangeRate);
		// 	}
		// }
		//
		// [ExchangeRate(0.6f, 1.4f)]
		// public float TetherExchangeRate
		// {
		// 	get => _tetherExchangeRate;
		// 	set
		// 	{
		// 		if (value < 0)
		// 			throw new ArgumentException();
		// 		_tetherExchangeRate = value;
		// 		OnRateChanged?.Invoke(nameof(Tether), TetherExchangeRate);
		// 	}
		// }
		//
		// [ExchangeRate(0.004f, 0.02f)]
		// public float DogecoinExchangeRate
		// {
		// 	get => _dogecoinExchangeRate;
		// 	set
		// 	{
		// 		if (value < 0)
		// 			throw new ArgumentException();
		// 		_dogecoinExchangeRate = value;
		// 		OnRateChanged?.Invoke(nameof(Dogecoin), DogecoinExchangeRate);
		// 	}
		// }
		//
		// [field: NonSerialized] public event Action<string, float> OnValueChanged;
		// [field: NonSerialized] public event Action<string, float> OnRateChanged;
	}

	public class CurrencyAttribute : Attribute
	{
		public float ExchangeRate
		{
			get
			{
				var cryptoData = StaticData.GetInstance().CryptoData;
				var rateProperty = cryptoData.GetType().GetProperty(_exchangeRateName);
				return (float) rateProperty?.GetValue(cryptoData)!;
			}
		}

		private readonly string _exchangeRateName;

		public CurrencyAttribute(string exchangeRateName)
		{
			_exchangeRateName = exchangeRateName;
		}
	}

	public class ExchangeRateAttribute : Attribute
	{
		public float Min { get; }
		public float Max { get; }

		public ExchangeRateAttribute(float minRate, float maxRate)
		{
			Min = minRate;
			Max = maxRate;
		}
	}
}