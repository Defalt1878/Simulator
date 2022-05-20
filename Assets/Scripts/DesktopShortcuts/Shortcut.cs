using System;
using TaskbarAndTasks;
using UnityEngine;

namespace DesktopShortcuts
{
	public class Shortcut : MonoBehaviour
	{
		[SerializeField] private Task task;
		private Task _instTask;

		public void OnClick()
		{
			if (task is null)
				throw new NullReferenceException(nameof(task));

			_instTask = TaskBar.GetInstance().AddOrExpandTask(task, _instTask);
		}

		private void OnDestroy()
		{
			if (_instTask is not null)
				TaskBar.GetInstance().EndTask(_instTask);
		}
	}
}