using Windows;
using UnityEngine;

namespace TaskbarAndTasks
{
	public abstract class Task : MonoBehaviour
	{
		private Window _window;
		private bool _isMinimized;
		private Canvas _windowCanvas;
		private TaskBar _taskBar;

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

		public void Awake()
		{
			_window = Resources.Load<Window>(TargetWindowPath);
			_window = Instantiate(_window, TaskBar.Desktop.transform);
			_windowCanvas = _window.gameObject.GetComponent<Canvas>();
			_window.currentTask = this;

			_taskBar = GetComponentInParent<TaskBar>();
		}

		public virtual void OnDestroy()
		{
			if (_window != null)
				Destroy(_window.gameObject);
		}

		public void OnClick()
		{
			if (_isMinimized || !_taskBar.TryMoveUpTask(this))
				IsMinimized = !IsMinimized;
		}

		private protected abstract string TargetWindowPath { get; }
	}
}