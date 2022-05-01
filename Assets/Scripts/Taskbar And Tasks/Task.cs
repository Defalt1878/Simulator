using Windows;
using UnityEngine;

namespace Taskbar_And_Tasks
{
	public abstract class Task : MonoBehaviour
	{
		private protected Window Window;
		private bool _isMinimized;

		public bool IsMinimized
		{
			get => _isMinimized;
			set
			{
				_isMinimized = value;
				var currentPos = (Vector2) Window.transform.position;
				if (_isMinimized)
					Window.transform.position = new Vector3(currentPos.x, currentPos.y, -1000);
				else
					Window.transform.position = currentPos;
			}
		}

		private void Start()
		{
			Window = Instantiate(Window, TaskBar.Desktop.transform);
			Window.currentTask = this;
		}

		public virtual void OnDestroy()
		{
			if (Window != null)
				Destroy(Window.gameObject);
		}

		public void OnClick() => IsMinimized = !IsMinimized;
	}
}