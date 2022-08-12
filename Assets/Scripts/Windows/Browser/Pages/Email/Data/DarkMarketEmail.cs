using System;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	[Serializable]
	public class DarkMarketEmail : EmailData
	{
		public override string SenderName => "DarkMarket";
		public override string Subject => "New purchase";

		public string Server { get; set; }
		public float HashRate { get; set; }

		public DarkMarketEmail(string server, float hashRate)
		{
			Server = server;
			HashRate = hashRate;
			IsCompleted = true;
		}

		public override void OnLoad()
		{
		}

		public override void OnOpen()
		{
			var instance = StaticData.GetInstance();
			if (IsRead)
				return;
			IsRead = true;
			instance.MiningData.NewAvailableServer(Server, HashRate);
		}

		private protected override string EmailFolder => "DarkMarket";
	}
}