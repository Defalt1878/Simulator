using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Taskbar_And_Tasks
{
	public class TaskBar : MonoBehaviour
	{
		public static GameObject Desktop { get; private set; }
		public List<Task> runningTasks;
		private Transform _items;
		private void Awake()
		{
			_items = GetComponentInChildren<GridLayoutGroup>().transform;
			runningTasks = new List<Task>();
			Desktop = GameObject.Find("Desktop");
		}

		public void AddOrExpandTask(Task task)
		{
			var runningTask = runningTasks.FirstOrDefault(runningTask => runningTask.GetType() == task.GetType());
			if (runningTask is null)
				runningTasks.Add(Instantiate(task, _items));
			else
				runningTask.IsMinimized = false;
		}

		public void EndTask(Task task)
		{
			runningTasks.Remove(task);
			Destroy(task.gameObject);
		}

		private void UpdateTaskBar()
		{
			// if (transform.childCount == runningTasks.Count)
			// 	return;

			foreach (var task in transform.GetComponentsInChildren<Task>())
				Destroy(task.gameObject);

			for (var i = 0; i < runningTasks.Count; i++)
				runningTasks[i] = Instantiate(runningTasks[i], _items);
		}
	}
}