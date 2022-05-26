using System;
using TaskbarAndTasks;
using UnityEngine;

namespace DesktopAndShortcuts
{
	public class Shortcut : MonoBehaviour
	{
		[SerializeField] private Task task;
		private Task _instTask;

		public void OnClick()
		{
			if (task is null)
				throw new NullReferenceException(nameof(task));

			_instTask = DesktopAndShortcuts.Desktop.Taskbar.AddOrExpandTask(task, _instTask);
		}

		private void OnDestroy()
		{
			if (_instTask is not null)
				DesktopAndShortcuts.Desktop.Taskbar.EndTask(_instTask);
		}
	}
}