using TaskbarAndTasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Windows
{
	public class Window : MonoBehaviour, IPointerClickHandler
	{
		public Task CurrentTask { get; set; }
		[SerializeField] public string winName;
		public void OnPointerClick(PointerEventData eventData) => CurrentTask.IsMinimized = false;
	}
}