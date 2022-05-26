using System;
using System.Collections.Generic;
using Notifications;
using UnityEngine;
using UserData;
using Random = UnityEngine.Random;

namespace Windows.Browser.Pages.DarkMarket
{
	public class LotsLoader : MonoBehaviour
	{
		[SerializeField] private PurchaseLot lotPrefab;
		[SerializeField] private PopUpNotification notification;
		private List<PurchaseLotInfo> _lotsInfo;

		private static readonly string[] Locations = {
			"Russia",
			"Netherlands",
			"Czech",
			"Germany",
			"Canada",
			"Belarus",
			"Kazakhstan"
		};

		public static Action<DateTime> AppearNewLot => _ =>
		{
			var name = "Remote Mining Server";
			var hashRate = Random.Range(50f, 300f);
			var location = Locations[Random.Range(0, Locations.Length)];
			var price = hashRate * Random.Range(0.8f, 1.2f);
			var lotInfo = new PurchaseLotInfo(name, location, hashRate, price);
			var availableLots = StaticData.GetInstance().MiningData.AvailableLots;
			availableLots.Add(lotInfo);
			if (availableLots.Count > 10)
				availableLots.RemoveAt(0);
		};

		private void Awake()
		{
			_lotsInfo = StaticData.GetInstance().MiningData.AvailableLots;
			foreach (var lotInfo in _lotsInfo)
			{
				var instLot = Instantiate(lotPrefab, transform);
				instLot.Info = lotInfo;
				instLot.Notification = notification;
			}
		}

		private void Update()
		{
			if (transform.childCount == _lotsInfo.Count)
				return;

			foreach (Transform child in transform)
				Destroy(child.gameObject);

			foreach (var lotInfo in _lotsInfo)
			{
				var instLot = Instantiate(lotPrefab, transform);
				instLot.Info = lotInfo;
				instLot.Notification = notification;
			}
		}
	}
}