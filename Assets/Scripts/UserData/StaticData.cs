using System;
using System.Collections.Generic;

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
			Shortcuts = new List<string> {"Browser"};
			AvailableToDownloadApps = new HashSet<string> {"Stats"};
			ReceivedEmails = new List<string> {"First"};
			CompletedEmails = new HashSet<string>();
			Stats = new GameStats();
		}

		public List<string> Shortcuts { get; private set; }
		public HashSet<string> AvailableToDownloadApps { get; private set; }
		public List<string> ReceivedEmails { get; private set; }
		public HashSet<string> CompletedEmails { get; private set; }
		public GameStats Stats { get; private set; }
	}
}