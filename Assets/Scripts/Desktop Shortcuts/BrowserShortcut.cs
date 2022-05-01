using System;
using System.IO;
using Taskbar_And_Tasks;
using UnityEngine;

namespace Desktop_Shortcuts
{
	public class BrowserShortcut : MonoBehaviour
	{
		private Task _task;

		private void Awake()
		{
			_task = Resources.Load<Task>(Path.Combine("TaskBar", "BrowserTask"));
		}

		public void OnClick()
		{
			if (!(FindObjectOfType(typeof(TaskBar)) is TaskBar taskBar))
				throw new NullReferenceException("TaskBarNotFound");

			taskBar.AddOrExpandTask(_task);
		}
	}
}