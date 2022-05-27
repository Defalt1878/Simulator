using System.Globalization;
using Notifications;
using TMPro;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.Crypto
{
	public class CryptoLine : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI cryptoName;
		[SerializeField] private TextMeshProUGUI cryptoAmount;
		[SerializeField] private TextMeshProUGUI exchangeRate;
		[SerializeField] private TMP_InputField buySellValue;
		[SerializeField] private TextMeshProUGUI cryptoToMoney;
		[SerializeField] private TextMeshProUGUI buttonText;
		public PopUpNotification Notification { get; set; }

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

		private float _cryptoAmount;

		public float CryptoAmount
		{
			get => _cryptoAmount;
			set
			{
				_cryptoAmount = value;
				cryptoAmount.text = $"{_cryptoAmount:0.####}";
			}
		}

		private float _exchangeRate;

		public float ExchangeRate
		{
			get => _exchangeRate;
			set
			{
				_exchangeRate = value;
				exchangeRate.text = $"{_exchangeRate:C}";
				CryptoToMoney = BuySellValue * ExchangeRate;
			}
		}

		private float _cryptoToMoney;

		public float CryptoToMoney
		{
			get => _cryptoToMoney;
			set
			{
				_cryptoToMoney = value;
				cryptoToMoney.text = $"= ${_cryptoToMoney:n}";
			}
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
			{
				Notification.Appear("Not enough money!", NotificationType.Warning);
				return;
			}

			stats.Money -= CryptoToMoney;

			var crypto = instance.CryptoData;
			var cryptoProperty = crypto.GetType().GetProperty(Name);
			cryptoProperty?.SetValue(crypto, CryptoAmount + BuySellValue);
		}

		private void TrySell()
		{
			if (CryptoAmount < BuySellValue)
			{
				Notification.Appear("Not enough crypto!", NotificationType.Warning);
				return;
			}

			var instance = StaticData.GetInstance();
			var stats = instance.Stats;
			stats.Money += CryptoToMoney;

			var crypto = instance.CryptoData;
			var cryptoProperty = crypto.GetType().GetProperty(Name);
			cryptoProperty?.SetValue(crypto, CryptoAmount - BuySellValue);
		}
	}
}