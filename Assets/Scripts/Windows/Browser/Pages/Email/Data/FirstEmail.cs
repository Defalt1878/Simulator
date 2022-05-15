using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	public class FirstEmail : EmailData
	{
		public override string SenderName => "Unknown";
		public override string Subject => "CMD";

		public override void OnOpen()
		{
			if (!StaticData.GetInstance().Shortcuts.Contains("CMD"))
				StaticData.GetInstance().AvailableToDownloadApps.Add("CMD");
		}

		private protected override string EmailFolder => "First";
	}
}