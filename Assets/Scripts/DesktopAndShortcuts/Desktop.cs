using Windows.Browser.Pages.Crypto;
using Windows.Browser.Pages.DarkMarket;
using Notifications;
using TaskbarAndTasks;
using UnityEngine;
using UserData;

namespace DesktopAndShortcuts
{
	public class Desktop : MonoBehaviour
	{
		[SerializeField] private TaskBar taskBar;
		[SerializeField] private PopUpNotification globalNotification;
		public static Desktop Instance { get; private set; }
		public static TaskBar Taskbar { get; private set; }
		public static PopUpNotification GlobalNotification { get; private set; }
		private int _receivedEmailsCount;

		private void Awake()
		{
			Instance = this;
			Taskbar = taskBar;
			GlobalNotification = globalNotification;
		}

		private void Start()
		{
			var clock = GetComponentInChildren<Clock>();
			clock.OnHourLast += LotsLoader.AppearNewLot;
			clock.OnHourLast += CryptoLoader.RandomizeExchangeRate;

			var received = StaticData.GetInstance().Emails;
			for (var i = _receivedEmailsCount; i < received.Count; i++)
				received[i].OnLoad();
			_receivedEmailsCount = received.Count;
		}

		private void Update()
		{
			var received = StaticData.GetInstance().Emails;
			for (var i = _receivedEmailsCount; i < received.Count; i++)
				received[i].OnLoad();

			if (_receivedEmailsCount != received.Count)
				globalNotification.Appear("New email!", NotificationType.Default);
			_receivedEmailsCount = received.Count;
		}
	}
}