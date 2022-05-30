using UnityEngine;
using UnityEngine.EventSystems;

namespace Windows.Panel
{
	public class DragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
	{
		private Vector2 _lastMousePos;
		private Window _window;
		private Camera _camera;

		private void Awake()
		{
			_camera = Camera.main;
			_window = GetComponentInParent<Window>();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			_lastMousePos = _camera.ScreenToWorldPoint(eventData.position);
			_window.CurrentTask.IsMinimized = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
			var mousePos = (Vector2) _camera.ScreenToWorldPoint(eventData.position);
			var mouseDelta = mousePos - _lastMousePos;
			_window.transform.position += (Vector3) mouseDelta;
			_lastMousePos = mousePos;
		}
	}
}