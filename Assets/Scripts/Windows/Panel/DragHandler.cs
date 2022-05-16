using UnityEngine;
using UnityEngine.EventSystems;

namespace Windows.Panel
{
	public class DragHandler : MonoBehaviour
	{
		private Vector2 _lastMousePos;
		private Window _window;
		private Camera _camera;

		private void Awake()
		{
			_camera = Camera.main;
			_window = GetComponentInParent<Window>();
		}

		public void OnDragStarted(BaseEventData eventData)
		{
			var pointerEventData = (PointerEventData) eventData;
			_lastMousePos = _camera.ScreenToWorldPoint(pointerEventData.position);
			_window.CurrentTask.IsMinimized = false;
		}

		public void OnDrag(BaseEventData eventData)
		{
			var pointerEventData = (PointerEventData) eventData;
			var mousePos = (Vector2) _camera.ScreenToWorldPoint(pointerEventData.position);
			var mouseDelta = mousePos - _lastMousePos;
			_window.transform.position += (Vector3) mouseDelta;
			_lastMousePos = mousePos;
		}
	}
}