using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.Crypto
{
	public class CryptoLoader : MonoBehaviour
	{
		[SerializeField] private CryptoLine linePrefab;
		private Dictionary<string, CryptoLine> _lines;
		private CryptoData _crypto;
		private Action<string, float> _onChangeAction;
		private CryptoPageState _state = CryptoPageState.Buy;

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
			foreach (var property in cryptoType.GetProperties().Where(prop => prop.Name[^4..] != "Rate"))
			{
				var instLine = Instantiate(linePrefab, transform);
				var propName = property.Name;
				instLine.Name = propName;
				instLine.Value = (float) property.GetValue(_crypto);
				instLine.ExchangeRate = (float) cryptoType.GetProperty($"{propName}ExchangeRate")?.GetValue(_crypto)!;
				instLine.State = _state;
				_lines[propName] = instLine;
			}

			_onChangeAction = (statName, newValue) =>
			{
				if (_lines.ContainsKey(statName))
					_lines[statName].Value = newValue;
			};
			_crypto.OnValueChanged += _onChangeAction;
		}

		private void OnDestroy()
		{
			_crypto.OnValueChanged -= _onChangeAction;
		}
	}
}