using System;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	[Serializable]
	public class MinerEmail : EmailData
	{
		public override string SenderName => "Unknown";
		public override string Subject => "Bitcoins";

		public override void OnLoad()
		{
			var instance = StaticData.GetInstance();
			if (IsCompleted)
				return;
			CheckComplete = (_, _) =>
			{
				//TODO
				OnComplete();
			};
			instance.Stats.OnValueChanged += CheckComplete;
		}

		public override void OnOpen()
		{
			var instance = StaticData.GetInstance();
			if (IsRead)
				return;
			IsRead = true;
			instance.Apps.AddToDownloads("Miner");
		}
		
		private void OnComplete()
		{
			var instance = StaticData.GetInstance();
			if (IsCompleted)
				return;
			IsCompleted = true;
			instance.Stats.OnValueChanged -= CheckComplete;
			//TODO
		}

		private protected override string EmailFolder => "Miner";
	}
}