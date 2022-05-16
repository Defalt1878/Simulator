using System;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	public class MinerEmail : EmailData
	{
		public override string SenderName => "Unknown";
		public override string Subject => "Bitcoins";
		private protected override Action OnComplete { get; }

		public override Action OnOpen => () =>
		{
			if (!StaticData.GetInstance().Shortcuts.Contains("Miner"))
				StaticData.GetInstance().AvailableToDownloadApps.Add("Miner");
		};

		private protected override string EmailFolder => "Miner";
	}
}