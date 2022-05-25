using System;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	[Serializable]
	public class CmdEmail : EmailData
	{
		public override string SenderName => "Unknown";
		public override string Subject => "CMD";

		public override void OnLoad()
		{
			var instance = StaticData.GetInstance();
			if (IsCompleted)
				return;
			CheckComplete = (_, _) =>
			{
				if (instance.Stats.Money >= 100)
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
			instance.Apps.AddToDownloads("CMD");
		}

		private void OnComplete()
		{
			var instance = StaticData.GetInstance();
			if (IsCompleted)
				return;
			IsCompleted = true;
			instance.Stats.OnValueChanged -= CheckComplete;
			instance.Emails.Add(new MinerEmail());
		}

		private protected override string EmailFolder => "Cmd";
	}
}