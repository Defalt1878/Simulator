using Windows.Browser.Pages.DarkMarket;
using Windows.Browser.Pages.Email;
using Taskbar;
using UnityEngine;

namespace Desktop
{
	public class Desktop : MonoBehaviour
	{
		private void Start()
		{
			foreach (var emailData in Inbox.EmailsData.Values)
				emailData(null).OnLoad();
			var clock = GetComponentInChildren<Clock>();
			clock.OnHourLast += LotsLoader.AppearNewLot;
		}
	}
}