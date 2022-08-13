using System;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	[Serializable]
	public class MinerEmail : EmailData
	{
		public override string SenderName => "Unknown";
		public override string Subject => "Bitcoins";
		[NonSerialized] private protected Action<int, float> CheckComplete;

		public override void OnLoad()
		{
			if (IsCompleted)
				return;
			CheckComplete = (_, hashRateValue) =>
			{
				if (hashRateValue >= 150)
					OnComplete();
			};
			StaticData.GetInstance().MiningData.OnNewServerConnected += CheckComplete;
		}

		public override void OnOpen()
		{
			var instance = StaticData.GetInstance();
			if (IsRead)
				return;
			IsRead = true;
			instance.Apps.AddToDownloads(App.Miner);
		}

		private void OnComplete()
		{
			var instance = StaticData.GetInstance();
			if (IsCompleted)
				return;
			IsCompleted = true;
			instance.MiningData.OnNewServerConnected -= CheckComplete;
			instance.Emails.Add(new SlotsEmail());
		}

		private protected override string EmailFolder => "Miner";
	}
}