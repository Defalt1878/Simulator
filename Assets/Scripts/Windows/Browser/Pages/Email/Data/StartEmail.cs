using Taskbar;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	public class StartEmail : EmailData
	{
		public static string Name => "Start";
		public override string SenderName => "Зубенко М. П.";
		public override string Subject => "Ну как там с деньгами?";

		public override void OnLoad()
		{
			var instance = StaticData.GetInstance();
			if (instance.Emails.IsCompleted(Name))
				return;
			CheckComplete = (_, _) =>
			{
				if (instance.Stats.Money >= 3000)
					OnComplete();
			};
			instance.Stats.OnValueChanged += CheckComplete;
			var clock = GameObject.Find("Desktop").GetComponentInChildren<Clock>();
			clock.OnHourLast += (currentTime) =>
			{
				if (instance.Emails.IsCompleted(Name))
					return;
				var timeSpan = currentTime - instance.StartTime;
				if (timeSpan.Hours >= 24)
					GameFailed();
			};
		}

		public override void OnOpen()
		{
			var instance = StaticData.GetInstance();
			if (instance.Emails.IsOpened(Name))
				return;
			instance.Emails.MarkOpen(Name);
			instance.StartTime = instance.CurrentTime;
			instance.Emails.NewEmail(CmdEmail.Name);
		}

		private void OnComplete()
		{
			var instance = StaticData.GetInstance();
			if (instance.Emails.IsCompleted(Name))
				return;
			instance.Emails.Complete(Name);
			instance.Stats.OnValueChanged -= CheckComplete;
			//TODO: Дописать конец игры
		}

		private void GameFailed()
		{
			//TODO: Дописать конец игры
		}

		private protected override string EmailFolder => "Start";
	}
}