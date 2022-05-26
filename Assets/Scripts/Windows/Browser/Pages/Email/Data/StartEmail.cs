using System;
using TaskbarAndTasks;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	[Serializable]
	public class StartEmail : EmailData
	{
		public override string SenderName => "Зубенко М. П.";
		public override string Subject => "Ну как там с деньгами?";

		public override void OnLoad()
		{
			var instance = StaticData.GetInstance();
			if (IsCompleted)
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
				if (IsCompleted)
					return;
				var timeSpan = currentTime - instance.StartTime;
				if (timeSpan.Hours >= 24)
					GameFailed();
			};
		}

		public override void OnOpen()
		{
			var instance = StaticData.GetInstance();
			if (IsRead)
				return;
			IsRead = true;
			instance.StartTime = instance.CurrentTime;
			instance.Emails.Add(new CmdEmail());
		}

		private void OnComplete()
		{
			var instance = StaticData.GetInstance();
			if (IsCompleted)
				return;
			IsCompleted = true;
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