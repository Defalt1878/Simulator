using UnityEngine;

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
			Window.gameObject.SetActive(!_isMinimized);
		}
	}

	private void Start()
	{
		Window = Instantiate(Window, Vector3.zero, Window.transform.rotation);
		Window.currentTask = this;
	}

	public virtual void OnDestroy()
	{
		Destroy(Window.gameObject);
	}

	public void OnMouseUpAsButton() => IsMinimized = !IsMinimized;
}