using System;
using Random = UnityEngine.Random;

namespace UserData
{
	[Serializable]
	public class Crypto : Stat<float>
	{
		public float ExchangeRate { get; private set; }
		private readonly float _minExchangeRate;
		private readonly float _maxExchangeRate;

		public Crypto(string name, float minExchangeRate, float maxExchangeRate)
			: base(name, "{0:0.####}", value => value >= 0)
		{
			if (minExchangeRate < 0 || maxExchangeRate < 0)
				throw new ArgumentException();

			_minExchangeRate = minExchangeRate;
			_maxExchangeRate = maxExchangeRate;
			ExchangeRate = Random.Range(minExchangeRate, maxExchangeRate);
		}

		public void RandomizeExchangeRate()
		{
			ExchangeRate = Random.Range(
				(float) Math.Max(ExchangeRate * 0.9, _minExchangeRate),
				(float) Math.Min(ExchangeRate * 1.1, _maxExchangeRate)
			);
			OnExchangeRateChanged?.Invoke(this);
		}

		public event Action<Crypto> OnExchangeRateChanged;

		public static float ToMoney(Crypto crypto, float value) =>
			crypto.ExchangeRate * value;
	}
}