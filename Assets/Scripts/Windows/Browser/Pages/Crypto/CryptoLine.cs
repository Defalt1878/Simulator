using System.Globalization;
using TMPro;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.Crypto
{
	public class CryptoLine : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI cryptoName;
		[SerializeField] private TextMeshProUGUI value;
		[SerializeField] private TextMeshProUGUI exchangeRate;
		[SerializeField] private TMP_InputField buySellValue;
		[SerializeField] private TextMeshProUGUI cryptoToMoney;
		[SerializeField] private TextMeshProUGUI buttonText;

		private CryptoPageState _state;

		public CryptoPageState State
		{
			get => _state;
			set
			{
				_state = value;
				buttonText.text = value == CryptoPageState.Buy
					? "Buy"
					: "Sell";
				BuySellValue = 0;
			}
		}

		public string Name
		{
			get => cryptoName.text;
			set => cryptoName.text = value;
		}

		public float Value
		{
			get => string.IsNullOrEmpty(value.text)
				? 0
				: float.Parse(value.text);
			set => this.value.text = $"{value:0.####}";
		}

		public float ExchangeRate
		{
			get => string.IsNullOrEmpty(exchangeRate.text)
				? 0
				: float.Parse(exchangeRate.text[..^2]);
			set => exchangeRate.text = $"{value:0.##} $";
		}

		public float CryptoToMoney
		{
			get => string.IsNullOrEmpty(cryptoToMoney.text)
				? 0
				: float.Parse(cryptoToMoney.text[2..^2]);
			set => cryptoToMoney.text = $"= {value:0.##} $";
		}

		private float BuySellValue
		{
			get =>
				string.IsNullOrEmpty(buySellValue.text)
					? 0
					: float.Parse(buySellValue.text);
			set => buySellValue.text = value.ToString(CultureInfo.InvariantCulture);
		}

		public void OnValueChange()
		{
			var text = buySellValue.text;
			if (text.Length > 0 && text[0] == '-')
				buySellValue.text = text.Remove(0, 1);
			
			CryptoToMoney = BuySellValue * ExchangeRate;
		}

		public void BuySellCrypto()
		{
			if (_state == CryptoPageState.Buy)
				TryBuy();
			else
				TrySell();
		}

		private void TryBuy()
		{
			var instance = StaticData.GetInstance();
			var stats = instance.Stats;
			if (stats.Money < CryptoToMoney)
				return;

			stats.Money -= CryptoToMoney;

			var crypto = instance.CryptoData;
			var cryptoProperty = crypto.GetType().GetProperty(Name);
			cryptoProperty?.SetValue(crypto, Value + BuySellValue);
		}

		private void TrySell()
		{
			if (Value < BuySellValue)
				return;
			var instance = StaticData.GetInstance();
			var stats = instance.Stats;
			stats.Money += CryptoToMoney;

			var crypto = instance.CryptoData;
			var cryptoProperty = crypto.GetType().GetProperty(Name);
			cryptoProperty?.SetValue(crypto, Value - BuySellValue);
		}
	}
}