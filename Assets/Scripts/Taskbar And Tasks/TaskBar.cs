using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Taskbar_And_Tasks
{
	public class TaskBar : MonoBehaviour
	{
		public static GameObject Desktop { get; private set; }
		public List<Task> runningTasks;
		private void Awake()
		{
			runningTasks = new List<Task>();
			Desktop = GameObject.Find("Desktop");
		}

		public void AddOrExpandTask(Task task)
		{
			var runningTask = runningTasks.FirstOrDefault(runningTask => runningTask.GetType() == task.GetType());
			if (runningTask is null)
				runningTasks.Add(Instantiate(task, transform));
			else
				runningTask.IsMinimized = false;
		}

		public void EndTask(Task task)
		{
			runningTasks.Remove(task);
			Destroy(task.gameObject);
		}
	}
}