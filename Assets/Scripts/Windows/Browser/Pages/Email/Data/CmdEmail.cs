using System;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	public class CmdEmail : EmailData
	{
		public static string Name => "Cmd";
		public override string SenderName => "Unknown";
		public override string Subject => "CMD";

		private void OnComplete()
		{
			var instance = StaticData.GetInstance();
			if (instance.Emails.IsCompleted(Name))
				return;
			instance.Emails.Complete(Name);
			instance.Stats.OnValueChanged -= CheckComplete;
			instance.Emails.NewEmail(MinerEmail.Name);
		}

		public override void OnOpen()
		{
			if (Opened)
				return;
			Opened = true;
			var instance = StaticData.GetInstance();
			if (instance.Emails.IsCompleted(Name))
				return;
			CheckComplete = (_, _) =>
			{
				if (instance.Stats.Money >= 100)
					OnComplete();
			};
			instance.Stats.OnValueChanged += CheckComplete;
			if (instance.Emails.IsOpened(Name))
				return;
			instance.Emails.MarkOpen(Name);
			instance.Apps.AddToDownloads("CMD");
			
		}

		private protected override string EmailFolder => "Cmd";
	}
}