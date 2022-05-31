using System;
using DesktopAndShortcuts;
using Final;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	[Serializable]
	public class StartEmail : EmailData
	{
		public override string SenderName => "Зубенко М. П.";
		public override string Subject => "Ну как там с деньгами?";
		public DateTime EndTime;
		private bool _finalPayEmailReceived;
		private bool _unknownFinalEmailReceived;
		[NonSerialized] private Action<DateTime> _onHourLastAction;

		public override void OnLoad()
		{
			if (IsCompleted)
				return;
			_onHourLastAction = currentTime =>
			{
				if (IsCompleted)
					return;
				var instance = StaticData.GetInstance();
				var timeSpan = currentTime - instance.StartTime;
				if (!_finalPayEmailReceived && timeSpan.TotalHours >= 21)
				{
					_finalPayEmailReceived = true;
					instance.Emails.Add(new FinalPayEmail());
				}

				if (!_unknownFinalEmailReceived && timeSpan.TotalHours >= 22)
				{
					_unknownFinalEmailReceived = true;
					instance.Emails.Add(new UnknownFinalEmail());
				}

				if (timeSpan.TotalHours >= 24)
					GameFailed();
			};

			Desktop.Clock.OnHourLast += _onHourLastAction;
		}

		public override void OnOpen()
		{
			var instance = StaticData.GetInstance();
			if (IsRead)
				return;
			IsRead = true;
			instance.StartTime = instance.CurrentTime;
			EndTime = instance.StartTime + new TimeSpan(24, 0, 0);
			instance.Emails.Add(new CmdEmail());
		}

		public void GameFinished()
		{
			IsCompleted = true;
			Desktop.Clock.OnHourLast -= _onHourLastAction;
		}

		private void GameFailed()
		{
			GameFinished();
			FinalScreens.GameFailedScreen.StartAnimation(DataSaver.ResetData);
		}

		private protected override string EmailFolder => "Start";
	}
}