using UnityEngine;

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

		private void OnMouseDown()
		{
			_lastMousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
			_window.CurrentTask.IsMinimized = false;
		}

		private void OnMouseDrag()
		{
			var mousePos = (Vector2) _camera.ScreenToWorldPoint(Input.mousePosition);
			var mouseDelta = mousePos - _lastMousePos;
			_window.transform.position += (Vector3) mouseDelta;
			_lastMousePos = mousePos;
		}
		
	}
}