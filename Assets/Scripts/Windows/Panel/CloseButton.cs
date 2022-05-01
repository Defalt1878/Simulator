using System;
using Windows.Browser;
using Taskbar_And_Tasks;
using UnityEngine;

namespace Windows.Panel
{
	public class CloseButton : MonoBehaviour
	{
		public void OnClick()
		{
			if (!(FindObjectOfType(typeof(TaskBar)) is TaskBar taskBar))
				throw new NullReferenceException("TaskBarNotFound");
		
			var task = gameObject.GetComponentInParent<BrowserWindow>().currentTask;

			taskBar.EndTask(task);
		}
	}
}