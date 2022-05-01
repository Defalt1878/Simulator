using UnityEngine;

namespace Windows.Panel
{
	public class DragHandler : MonoBehaviour
	{
		private Vector2 _lastMousePos;
		private Transform _draggingObject;
		private Camera _camera;

		private void Awake()
		{
			_camera = Camera.main;
			_draggingObject = GetComponentInParent<Window>().transform;
		}

		private void OnMouseDown()
		{
			_lastMousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
		}

		private void OnMouseDrag()
		{
			var mousePos = (Vector2) _camera.ScreenToWorldPoint(Input.mousePosition);
			var mouseDelta = mousePos - _lastMousePos;
			_draggingObject.position += (Vector3) mouseDelta;
			_lastMousePos = mousePos;
		}
		
	}
}