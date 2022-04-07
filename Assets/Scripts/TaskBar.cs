using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskBar : MonoBehaviour
{
	public List<Task> runningTasks;

	private void Awake()
	{
		runningTasks = new List<Task>();
	}

	public void AddOrExpandTask(Task task)
	{
		var runningTask = runningTasks.FirstOrDefault(runningTask => runningTask.GetType() == task.GetType());
		if (runningTask is null)
			runningTasks.Add(task);
		else
			runningTask.IsMinimized = false;
	}

	public void EndTask(Task task)
	{
		runningTasks.Remove(task);
	}

	private void Update()
	{
		if (transform.childCount == runningTasks.Count)
			return;

		foreach (var task in GameObject.FindGameObjectsWithTag("TaskBarItem"))
			Destroy(task);

		var firstTask = runningTasks.FirstOrDefault();
		if (firstTask is null)
			return;

		var width = firstTask.GetComponent<RectTransform>().rect.width;
		var indent = firstTask.transform.localPosition.x - width / 2;

		for (var i = 0; i < runningTasks.Count; i++)
		{
			runningTasks[i] = Instantiate(runningTasks[i], transform);

			runningTasks[i].transform.localPosition += Vector3.right * ((width + indent) * i);
		}
	}
}