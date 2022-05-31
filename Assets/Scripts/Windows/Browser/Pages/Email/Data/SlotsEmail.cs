using System;

namespace Windows.Browser.Pages.Email.Data
{
	[Serializable]
	public class SlotsEmail : EmailData
	{
		public override string SenderName => "Developer";
		public override string Subject => "Проблемки с контентом.";
		public override void OnLoad()
		{
			
		}

		public override void OnOpen()
		{
			IsRead = true;
			IsCompleted = true;
		}

		private protected override string EmailFolder => "Slots";
	}
}