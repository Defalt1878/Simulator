using Windows;
using UnityEngine;

namespace TaskbarAndTasks
{
	public class Task : MonoBehaviour
	{
		public bool IsMinimized
		{
			get => _isMinimized;
			set
			{
				_isMinimized = value;
				_windowCanvas.enabled = !_isMinimized;
				if (!_isMinimized)
					_taskBar.TryMoveUpTask(this);
			}
		}
		
		public int Priority
		{
			get => _windowCanvas.sortingOrder;
			set => _windowCanvas.sortingOrder = value;
		}

		[SerializeField]
		public Window window;
		private bool _isMinimized;
		private Canvas _windowCanvas;
		private TaskBar _taskBar;

		public void Awake()
		{
			window = Instantiate(window, TaskBar.Desktop.transform);
			window.CurrentTask = this;
			_windowCanvas = window.gameObject.GetComponent<Canvas>();

			_taskBar = GetComponentInParent<TaskBar>();
		}

		public void OnDestroy()
		{
			if (window != null)
				Destroy(window.gameObject);
		}

		public void OnClick()
		{
			if (_isMinimized || !_taskBar.TryMoveUpTask(this))
				IsMinimized = !IsMinimized;
		}
	}
}