using System;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	[Serializable]
	public class CmdEmail : EmailData
	{
		public static string Name => "Cmd";
		public override string SenderName => "Unknown";
		public override string Subject => "CMD";

		public override void OnLoad()
		{
			var instance = StaticData.GetInstance();
			if (instance.Emails.IsCompleted(Name))
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
			if (instance.Emails.IsRead(Name))
				return;
			instance.Emails.MarkAsRead(Name);
			instance.Apps.AddToDownloads("CMD");
		}

		private void OnComplete()
		{
			var instance = StaticData.GetInstance();
			if (instance.Emails.IsCompleted(Name))
				return;
			instance.Emails.Complete(Name);
			instance.Stats.OnValueChanged -= CheckComplete;
			instance.Emails.NewEmail(MinerEmail.Name);
		}

		private protected override string EmailFolder => "Cmd";
	}
}