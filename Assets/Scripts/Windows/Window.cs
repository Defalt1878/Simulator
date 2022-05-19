using TaskbarAndTasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Windows
{
	public abstract class Window : MonoBehaviour
	{
		public Task CurrentTask { get; set; }
		public abstract string Name { get; }

		public void OnClick(BaseEventData data) => CurrentTask.IsMinimized = false;
	}
}