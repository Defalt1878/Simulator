using System;
using Taskbar_And_Tasks;
using UnityEngine;

namespace Windows.Panel
{
	public class CloseButton : MonoBehaviour
	{
		public void OnClick()
		{
			if (FindObjectOfType(typeof(TaskBar)) is not TaskBar taskBar)
				throw new NullReferenceException("TaskBarNotFound");
		
			var task = gameObject.GetComponentInParent<Window>().currentTask;

			taskBar.EndTask(task);
		}
	}
}