using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Browser.Pages.Email.Data;
using Notifications;
using TMPro;
using UnityEngine;
using UserData;
using Random = UnityEngine.Random;

namespace Windows.Browser.Pages.DarkMarket
{
	public class PurchaseLot : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI lotName;
		[SerializeField] private TextMeshProUGUI lotInfo;
		[SerializeField] private TextMeshProUGUI buttonText;
		public PopUpNotification Notification { get; set; }
		private List<PurchaseLotInfo> _lotsInfo;

		private PurchaseLotInfo _info;

		public PurchaseLotInfo Info
		{
			get => _info;
			set
			{
				_info = value;
				lotName.text = _info.Name;
				lotInfo.text = _info.Info;
				buttonText.text = $"{Info.Price:0.##} $";
			}
		}

		private void Awake()
		{
			//_lotsInfo = StaticData.GetInstance().MiningData.AvailableLots; TODO
		}

		public void Buy()
		{
			var instance = StaticData.GetInstance();
			if (instance.Stats.Money.Value < _info.Price)
			{
				Notification.Appear("Not enough money!", NotificationType.Warning);
				return;
			}

			instance.Stats.Money.Value -= _info.Price;
			_lotsInfo.Remove(_info);
			instance.Emails.Add(new DarkMarketEmail(GenerateAddress(6), _info.HashRate));
			Notification.Appear("Purchase successful!", NotificationType.Success);
		}

		private static string GenerateAddress(int length)
		{
			return string.Join("",
				Enumerable.Range(0, length)
					.Select(_ => Random.Range('A', 'Z'))
					.Select(Convert.ToChar)
			);
		}
	}
}