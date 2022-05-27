using System;
using System.Collections.Generic;
using System.Reflection;
using Notifications;
using UnityEngine;
using UserData;
using Random = UnityEngine.Random;

namespace Windows.Browser.Pages.Crypto
{
	public class CryptoLoader : MonoBehaviour
	{
		[SerializeField] private CryptoLine linePrefab;
		[SerializeField] private PopUpNotification notification;
		private Dictionary<string, CryptoLine> _lines;
		private CryptoData _crypto;
		private Action<string, float> _onValueChangeAction;
		private Action<string, float> _onRateChangeAction;
		private CryptoPageState _state = CryptoPageState.Buy;

		public static readonly Action<DateTime> RandomizeExchangeRate = _ =>
		{
			var cryptoData = StaticData.GetInstance().CryptoData;
			foreach (var property in cryptoData.GetType().GetProperties())
			{
				if (property.GetCustomAttribute<ExchangeRateAttribute>() is not { } exchangeRateAtt)
					continue;
				var currentValue = (float) property.GetValue(cryptoData);
				var newValue = Random.Range(
					(float) Math.Max(currentValue * 0.9, exchangeRateAtt.Min),
					(float) Math.Min(currentValue * 1.1, exchangeRateAtt.Max)
				);
				property.SetValue(cryptoData, newValue);
			}
		};

		public void SetBuyState()
		{
			_state = CryptoPageState.Buy;
			UpdateLinesState();
		}

		public void SetSellState()
		{
			_state = CryptoPageState.Sell;
			UpdateLinesState();
		}

		private void UpdateLinesState()
		{
			foreach (var line in _lines.Values)
				line.State = _state;
		}

		private void Awake()
		{
			_lines = new Dictionary<string, CryptoLine>();
			_crypto = StaticData.GetInstance().CryptoData;
			var cryptoType = _crypto.GetType();
			foreach (var property in cryptoType.GetProperties())
			{
				var currencyAtt = property.GetCustomAttribute<CurrencyAttribute>();
				if (currencyAtt is null)
					continue;
				var instLine = Instantiate(linePrefab, transform);
				var propName = property.Name;
				instLine.Name = propName;
				instLine.CryptoAmount = (float) property.GetValue(_crypto);
				instLine.ExchangeRate = currencyAtt.ExchangeRate;
				instLine.State = _state;
				instLine.Notification = notification;
				_lines[propName] = instLine;
			}

			_onValueChangeAction = (cryptoName, newValue) =>
			{
				if (_lines.ContainsKey(cryptoName))
					_lines[cryptoName].CryptoAmount = newValue;
			};
			_crypto.OnValueChanged += _onValueChangeAction;
			_onRateChangeAction = (cryptoName, newValue) =>
			{
				if (_lines.ContainsKey(cryptoName))
					_lines[cryptoName].ExchangeRate = newValue;
			};
			_crypto.OnRateChanged += _onRateChangeAction;
		}

		private void OnDestroy()
		{
			_crypto.OnValueChanged -= _onValueChangeAction;
			_crypto.OnRateChanged -= _onRateChangeAction;
		}
	}
}