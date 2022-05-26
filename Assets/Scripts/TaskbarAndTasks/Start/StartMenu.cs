using DesktopAndShortcuts;
using Notifications;
using UnityEngine;
using UserData;

namespace TaskbarAndTasks.Start
{
	public class StartMenu : MonoBehaviour
	{
		private PopUpNotification _notification;

		private void Awake()
		{
			_notification = Desktop.GlobalNotification;
		}

		public void ExitGame()
		{
			Application.Quit();
		}
		
		public void SaveData()
		{
			DataSaver.SaveData();
			_notification.Appear("Game saved.", NotificationType.Success);
		}
		
		public void ResetData()
		{
			DataSaver.ResetData();
			_notification.Appear("Game was reset.", NotificationType.Default);
		}
	}
}
