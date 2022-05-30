using Windows;
using DesktopAndShortcuts;
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
				_windowAnimator.SetBool(Minimized, _isMinimized);
				if (!_isMinimized)
					_taskBar.TryMoveUpTask(this);
			}
		}

		public int Priority
		{
			get => _windowCanvas.sortingOrder;
			set => _windowCanvas.sortingOrder = value;
		}

		[SerializeField] public Window window;
		private Animator _windowAnimator;
		private bool _isMinimized;
		private Canvas _windowCanvas;
		private TaskBar _taskBar;
		private static readonly int Minimized = Animator.StringToHash("Minimized");

		public void Awake()
		{
			window = Instantiate(window, Desktop.Instance.transform);
			window.CurrentTask = this;
			_windowAnimator = window.GetComponent<Animator>();
			_windowCanvas = window.GetComponent<Canvas>();
		
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