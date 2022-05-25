using Windows.Browser.Pages.Crypto;
using Windows.Browser.Pages.DarkMarket;
using Taskbar;
using UnityEngine;
using UserData;

namespace Desktop
{
	public class Desktop : MonoBehaviour
	{
		private int _receivedEmailsCount;

		private void Start()
		{
			var clock = GetComponentInChildren<Clock>();
			clock.OnHourLast += LotsLoader.AppearNewLot;
			clock.OnHourLast += CryptoLoader.RandomizeExchangeRate;
		}

		private void Update()
		{
			var received = StaticData.GetInstance().Emails;
			for (var i = _receivedEmailsCount; i < received.Count; i++)
				received[i].OnLoad();

			_receivedEmailsCount = received.Count;
		}
	}
}