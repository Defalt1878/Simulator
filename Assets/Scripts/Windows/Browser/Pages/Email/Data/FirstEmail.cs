using System;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	public class FirstEmail : EmailData
	{
		public override string SenderName => "Unknown";
		public override string Subject => "CMD";

		private protected override Action OnComplete => () =>
		{
			var data = StaticData.GetInstance();
			data.ReceivedEmails.Add("Miner");
			data.CompletedEmails.Add("First");
		};

		public override Action OnOpen => () =>
		{
			var data = StaticData.GetInstance();
			if (!data.Shortcuts.Contains("CMD"))
				data.AvailableToDownloadApps.Add("CMD");

			if (!data.CompletedEmails.Contains("First"))
				data.Stats.OnValueChanged += CheckComplete;

			void CheckComplete(string name, string _)
			{
				if (name != "Money" || data.Stats.Money < 100)
					return;
				OnComplete.Invoke();
				data.Stats.OnValueChanged -= CheckComplete;
			}
		};

		private protected override string EmailFolder => "First";
	}
}