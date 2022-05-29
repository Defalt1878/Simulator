using System;

namespace Windows.Browser.Pages.Email.Data
{
	[Serializable]
	public class UnknownFinalEmail : EmailData
	{
		public override string SenderName => "Unknown";
		public override string Subject => "Save your ass";
		public override void OnLoad()
		{
		}

		public override void OnOpen()
		{
			IsRead = true;
			IsCompleted = true;
		}

		private protected override string EmailFolder => "UnknownFinal";
	}
}