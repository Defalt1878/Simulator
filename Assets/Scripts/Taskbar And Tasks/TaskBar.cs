using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Taskbar_And_Tasks
{
	public class TaskBar : MonoBehaviour
	{
		public static GameObject Desktop { get; private set; }
		public List<Task> runningTasks;
		public List<Task> runningTasksOrder;

		private void Awake()
		{
			runningTasks = new List<Task>();
			Desktop = GameObject.Find("Desktop");
		}

		public void AddOrExpandTask(Task task)
		{
			var runningTask = runningTasks.FirstOrDefault(runningTask => runningTask.GetType() == task.GetType());
			if (runningTask is null)
			{
				task = Instantiate(task, transform);
				runningTasks.Add(task);
				runningTasksOrder.Add(task);
				task.Priority = runningTasksOrder.Count - 1;
			}
			else
			{
				runningTask.OnClick();
			}
		}

		public void EndTask(Task task)
		{
			runningTasks.Remove(task);
			runningTasksOrder.Remove(task);
			SetTasksOrder();
			Destroy(task.gameObject);
		}

		public bool TryMoveUpTask(Task task)
		{
			var lastTask = runningTasksOrder.LastOrDefault(t => !t.IsMinimized);
			if (lastTask is null || lastTask == task)
				return false;

			for (var i = 0; i < runningTasksOrder.Count - 1; i++)
				if (runningTasksOrder[i] == task)
					(runningTasksOrder[i], runningTasksOrder[i + 1]) = (runningTasksOrder[i + 1], runningTasksOrder[i]);
			SetTasksOrder();

			return true;
		}

		private void SetTasksOrder()
		{
			for (var i = 0; i < runningTasksOrder.Count; i++)
				runningTasksOrder[i].Priority = i;
		}
	}
}