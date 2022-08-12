using System;
using System.Globalization;
using System.Linq;
using Notifications;
using TMPro;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.CryptoMarket
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

		private CryptoTrader _trader;

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
				UserValueInput = 0;
			}
		}

		private Crypto _crypto;

		public Crypto Crypto
		{
			get => _crypto;
			set
			{
				_crypto = value ?? throw new ArgumentNullException();
				cryptoName.text = _crypto.Name;
				cryptoAmount.text = _crypto.StrValue;
				exchangeRate.text = $"${_crypto.ExchangeRate:n}";
				_trader = new CryptoTrader(_crypto);

				_crypto.OnValueChanged += _onValueChanged = crypto => cryptoAmount.text = crypto.StrValue;
				_crypto.OnExchangeRateChanged +=
					_onExchangeRateChanged = crypto => exchangeRate.text = $"${crypto.ExchangeRate:n}";
			}
		}

		private float CryptoToMoney
		{
			set => cryptoToMoney.text = $"= ${value:n}";
		}

		private float UserValueInput
		{
			get =>
				string.IsNullOrEmpty(buySellValue.text)
					? 0
					: float.Parse(buySellValue.text);
			set => buySellValue.text = value.ToString(CultureInfo.InvariantCulture);
		}

		// ReSharper disable once UnusedMember.Global
		public void OnValueChange()
		{
			var text = buySellValue.text;
			if (text.FirstOrDefault() == '-')
				buySellValue.text = text.Remove(0, 1);

			CryptoToMoney = Crypto.ToMoney(Crypto, UserValueInput);
		}

		// ReSharper disable once UnusedMember.Global
		public void BuySellCrypto()
		{
			var result = _trader.TryTradeCrypto(UserValueInput, _state);
			if (result is { })
				Notification.Appear(result, NotificationType.Warning);
		}


		private Action<IStat> _onValueChanged;
		private Action<Crypto> _onExchangeRateChanged;

		private void OnDestroy()
		{
			Crypto.OnValueChanged -= _onValueChanged;
			Crypto.OnExchangeRateChanged -= _onExchangeRateChanged;
		}
	}
}