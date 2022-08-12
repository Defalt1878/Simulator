using DesktopAndShortcuts;
using TMPro;
using UnityEngine;

namespace Windows.Panel
{
	public class WindowPanel : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI windowName;
		private Vector2 _lastMousePos;

		private void Start()
		{
			windowName.text = GetComponentInParent<Window>().winName;
		}

		public void Minimize() =>
			gameObject.GetComponentInParent<Window>().CurrentTask.IsMinimized = true;

		public void Close()
		{
			var taskBar = Desktop.Taskbar;
			var task = gameObject.GetComponentInParent<Window>().CurrentTask;

			taskBar.EndTask(task);
		}
	}
}