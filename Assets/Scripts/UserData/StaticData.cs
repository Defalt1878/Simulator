using System;
using System.Collections.Generic;
using Windows.Browser.Pages.Email.Data;
using Random = UnityEngine.Random;

namespace UserData
{
	[Serializable]
	public class StaticData
	{
		[NonSerialized] private static StaticData _instance;

		public static StaticData GetInstance()
		{
			if (_instance is not null)
				return _instance;
			_instance = new StaticData();
			DataSaver.LoadData();
			return _instance;
		}

		private StaticData()
		{
			Apps = new AppsData(new[] {"Browser"}, new[] {"Stats"});
			Emails = new List<EmailData>() {new StartEmail()};
			Stats = new GameStats
			{
				Money = Random.Range(0, 30)
			};
			CryptoData = new CryptoData();
			MiningData = new MiningData();
			CurrentTime = new DateTime(2022, 7, 1, 0, 0, 0);
			StartTime = CurrentTime;
		}

		public AppsData Apps { get; private set; }
		public List<EmailData> Emails { get; private set; }
		public GameStats Stats { get; private set; }
		public CryptoData CryptoData { get; private set; }
		public MiningData MiningData { get; set; }
		public DateTime CurrentTime { get; set; }
		public DateTime StartTime { get; set; }
	}
}