using System;
using TaskbarAndTasks;
using UnityEngine;
using UserData;

namespace DesktopAndShortcuts
{
	public class Shortcut : MonoBehaviour
	{
		[SerializeField] private Task task;
		private Task _instTask;

		public App App => App.Parse(name);

		public void OnClick()
		{
			if (task is null)
				throw new NullReferenceException(nameof(task));

			_instTask = Desktop.Taskbar.AddOrExpandTask(task, _instTask);
		}

		private void OnDestroy()
		{
			if (_instTask is not null)
				Desktop.Taskbar.EndTask(_instTask);
		}
	}
}