using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	public class DarkMarketEmail : EmailData
	{
		public static string Name => "DarkMarket";

		private string FullName => $"{Name} {HashRate} {Server}";
		public override string SenderName => "DarkMarket";
		public override string Subject => "New purchase";
		
		public string Server { get; set; }
		public float HashRate { get; set; }

		public override void OnLoad()
		{
		}

		public override void OnOpen()
		{
			var instance = StaticData.GetInstance();
			if (instance.Emails.IsRead(FullName))
				return;
			instance.Emails.MarkAsRead(FullName);
			instance.MiningData.ServersHashRates[Server] = HashRate;
		}

		private protected override string EmailFolder => "DarkMarket";
	}
}