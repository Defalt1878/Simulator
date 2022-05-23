using System;
using Taskbar;
using TMPro;
using UnityEngine;

namespace Windows.Panel
{
	public class WindowPanel : MonoBehaviour
	{
		private Vector2 _lastMousePos;

		private void Awake()
		{
			var text = GetComponentInChildren<TextMeshProUGUI>();
			text.text = GetComponentInParent<Window>().winName;
		}

		public void Minimize() =>
			gameObject.GetComponentInParent<Window>().CurrentTask.IsMinimized = true;

		public void Close()
		{
			if (FindObjectOfType(typeof(TaskBar)) is not TaskBar taskBar)
				throw new NullReferenceException("TaskBarNotFound");

			var task = gameObject.GetComponentInParent<Window>().CurrentTask;

			taskBar.EndTask(task);
		}
	}
}