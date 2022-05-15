using System;
using TaskbarAndTasks;
using UnityEngine;

namespace DesktopShortcuts
{
	public class Shortcut : MonoBehaviour
	{
		[SerializeField]
		private Task task;

		public void OnClick()
		{
			if (task is null)
				throw new NullReferenceException(nameof(task));
			if (FindObjectOfType(typeof(TaskBar)) is not TaskBar taskBar)
				throw new NullReferenceException("TaskBarNotFound");
			
			taskBar.AddOrExpandTask(task);
		}
	}
}