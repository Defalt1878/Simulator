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
}