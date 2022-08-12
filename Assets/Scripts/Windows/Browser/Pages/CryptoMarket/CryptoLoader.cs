using System;
using System.Collections.Generic;
using System.Linq;
using Notifications;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.CryptoMarket
{
	public class CryptoLoader : MonoBehaviour
	{
		[SerializeField] private CryptoLine linePrefab;
		[SerializeField] private PopUpNotification notification;

		private List<CryptoLine> _lines;
		private CryptoPageState _state = CryptoPageState.Buy;

		private void Start()
		{
			_lines = new List<CryptoLine>();
			var cryptoData = StaticData.GetInstance().CryptoData;
			foreach (var crypto in cryptoData.GetType().GetProperties()
				         .Select(property => property.GetValue(cryptoData) as Crypto)
				         .Where(crypto => crypto is not null)
			        )
			{
				var instLine = Instantiate(linePrefab, transform);
				instLine.Crypto = crypto;
				instLine.State = _state;
				instLine.Notification = notification;
				_lines.Add(instLine);
			}
		}

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
			foreach (var line in _lines)
				line.State = _state;
		}

		public static readonly Action<DateTime> RandomizeExchangeRate = _ =>
		{
			foreach (var crypto in typeof(CryptoData).GetProperties()
				         .Select(p => p.GetValue(StaticData.GetInstance().CryptoData) as Crypto)
				         .Where(crypto => crypto is not null)
			        )
				crypto.RandomizeExchangeRate();
		}; // TODO перенести это нахуй отсюда
	}
}