using System;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
	public void OnMouseUpAsButton()
	{
		if (!(FindObjectOfType(typeof(TaskBar)) is TaskBar taskBar))
			throw new NullReferenceException("TaskBarNotFound");
		
		var task = gameObject.GetComponentInParent<BrowserWindow>().currentTask;

		taskBar.EndTask(task);
	}
}