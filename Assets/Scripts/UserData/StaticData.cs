using System;
using System.Collections.Generic;
using Windows.Browser.Pages.Email.Data;
using UserData.DataSavers;

namespace UserData
{
	[Serializable]
	public class StaticData
	{
		[NonSerialized] private static IDataSaver _dataSaver = new BinaryDataSaver();
		public static IDataSaver DataSaver => _dataSaver;

		[NonSerialized] private static StaticData _instance;

		public static StaticData GetInstance() =>
			_instance ??= _dataSaver.LoadData() ?? new StaticData();

		private StaticData()
		{
			Stats = new UserStats();
			MiningData = new MiningData();
			CryptoData = new CryptoData();
			Emails = new List<EmailData> {new StartEmail()};
			Apps = new AppsData(new[] {App.Browser}, new[] {App.Stats});
			CurrentInGameTime = new DateTime(2022, 7, 1, 0, 0, 0);
		}

		public UserStats Stats { get; private set; }
		public MiningData MiningData { get; private set; }
		public CryptoData CryptoData { get; private set; }
		public AppsData Apps { get; private set; }
		public List<EmailData> Emails { get; private set; }
		public DateTime CurrentInGameTime { get; set; }
		public DateTime StartTime { get; set; }
	}
}