using System.Linq;
using Windows.Browser.Pages.Crypto;
using Windows.Browser.Pages.DarkMarket;
using Windows.Browser.Pages.Email;
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
			var received = StaticData.GetInstance().Emails.GetReceived();
			for (var i = _receivedEmailsCount; i < received.Count; i++)
			{
				var emailName = received[i].Split(' ').First();
				Inbox.EmailsData[emailName].Invoke(null).OnLoad();
			}

			_receivedEmailsCount = received.Count;
		}
	}
}