using TaskbarAndTasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Windows
{
	public class Window : MonoBehaviour
	{
		public Task CurrentTask { get; set; }
		[SerializeField] public string winName;

		public void OnClick(BaseEventData data) => CurrentTask.IsMinimized = false;
	}
}