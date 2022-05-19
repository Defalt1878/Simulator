using System;
using TaskbarAndTasks;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	public class StartEmail : EmailData
	{
		public static string Name => "Start";
		public override string SenderName => "Зубенко М. П.";
		public override string Subject => "Ну как там с деньгами?";

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
				if (instance.Stats.Money >= 3000)
					OnComplete();
			};
			instance.Stats.OnValueChanged += CheckComplete;
			var clock = TaskBar.Desktop.GetComponentInChildren<Clock>();
			clock.OnStop += () =>
			{
				if (instance.Emails.IsCompleted(Name))
					return;
				GameFailed();
			};
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