using System;

namespace Windows.Browser.Pages.Email.Data
{
	[Serializable]
	public class FinalPayEmail : EmailData
	{
		public override string SenderName => "Зубенко М. П.";
		public override string Subject => "Ну как там с деньгами?";

		public override void OnLoad()
		{
		}

		public override void OnOpen()
		{
			IsRead = true;
			IsCompleted = true;
		}

		private protected override string EmailFolder => "FinalPay";
	}
}