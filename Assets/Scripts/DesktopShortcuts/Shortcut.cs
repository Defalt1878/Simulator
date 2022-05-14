using System;
using System.IO;
using TaskbarAndTasks;
using UnityEngine;

namespace DesktopShortcuts
{
	public abstract class Shortcut : MonoBehaviour
	{
		private protected abstract string TaskName { get; }

		[NonSerialized] private Task _task;

		private void Awake()
		{
			_task = Resources.Load<Task>(Path.Combine("TaskBar", TaskName));
		}

		public void OnClick()
		{
			if (FindObjectOfType(typeof(TaskBar)) is not TaskBar taskBar)
				throw new NullReferenceException("TaskBarNotFound");

			taskBar.AddOrExpandTask(_task);
		}
	}
}