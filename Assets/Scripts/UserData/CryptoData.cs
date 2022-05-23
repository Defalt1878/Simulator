using System;

namespace UserData
{
	[Serializable]
	public class CryptoData
	{
		private float _bitcoin;
		private float _ethereum;
		private float _tether;
		private float _dogecoin;

		[Currency(nameof(BitcoinExchangeRate))]
		public float Bitcoin
		{
			get => _bitcoin;
			set
			{
				if (value < 0)
					throw new ArgumentException();
				_bitcoin = value;
				OnValueChanged?.Invoke(nameof(Bitcoin), Bitcoin);
			}
		}

		[Currency(nameof(EthereumExchangeRate))]
		public float Ethereum
		{
			get => _ethereum;
			set
			{
				if (value < 0)
					throw new ArgumentException();
				_ethereum = value;
				OnValueChanged?.Invoke(nameof(Ethereum), Ethereum);
			}
		}

		[Currency(nameof(TetherExchangeRate))]
		public float Tether
		{
			get => _tether;
			set
			{
				if (value < 0)
					throw new ArgumentException();
				_tether = value;
				OnValueChanged?.Invoke(nameof(Tether), Tether);
			}
		}

		[Currency(nameof(DogecoinExchangeRate))]
		public float Dogecoin
		{
			get => _dogecoin;
			set
			{
				if (value < 0)
					throw new ArgumentException();
				_dogecoin = value;
				OnValueChanged?.Invoke(nameof(Dogecoin), Dogecoin);
			}
		}

		public float BitcoinExchangeRate => 30296.90f;
		public float EthereumExchangeRate => 2040.40f;
		public float TetherExchangeRate => 1.00f;
		public float DogecoinExchangeRate => 0.087f;

		[field: NonSerialized] public event Action<string, float> OnValueChanged;
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
}