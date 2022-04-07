using UnityEngine;

public class MinimizeButton : MonoBehaviour
{
	public void OnMouseUpAsButton()
	{
		gameObject.GetComponentInParent<BrowserWindow>().currentTask.IsMinimized = true;
	}
}