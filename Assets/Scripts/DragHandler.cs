using UnityEngine;

public class DragHandler : MonoBehaviour
{
	private Vector2 _lastMousePos;
	private Transform _draggingObject;

	private void Awake()
	{
		_draggingObject = GetComponentInParent<Window>().transform;
	}

	private void OnMouseDown()
	{
		_lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private void OnMouseDrag()
	{
		var mousePos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var mouseDelta = mousePos - _lastMousePos;
		_draggingObject.position += (Vector3) mouseDelta;
		_lastMousePos = mousePos;
	}
}