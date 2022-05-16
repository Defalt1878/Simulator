using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TaskbarAndTasks
{
	public class TaskBar : MonoBehaviour
	{
		public static GameObject Desktop { get; private set; }
		private List<Task> _runningTasks;
		private List<Task> _runningTasksOrder;

		private void Awake()
		{
			_runningTasks = new List<Task>();
			_runningTasksOrder = new List<Task>();
			Desktop = GameObject.Find("Desktop");
		}

		public void AddOrExpandTask(Task task)
		{
			var runningTask = _runningTasks
				.FirstOrDefault(runningTask => runningTask.window.GetType() == task.window.GetType());
			if (runningTask is null)
			{
				task = Instantiate(task, transform);
				_runningTasks.Add(task);
				_runningTasksOrder.Add(task);
				task.Priority = _runningTasksOrder.Count - 1;
			}
			else
			{
				runningTask.OnClick();
			}
		}

		public void EndTask(Task task)
		{
			_runningTasks.Remove(task);
			_runningTasksOrder.Remove(task);
			SetTasksOrder();
			Destroy(task.gameObject);
		}

		public bool TryMoveUpTask(Task task)
		{
			var lastTask = _runningTasksOrder.LastOrDefault(t => !t.IsMinimized);
			if (lastTask is null || lastTask == task)
				return false;

			for (var i = 0; i < _runningTasksOrder.Count - 1; i++)
				if (_runningTasksOrder[i] == task)
					(_runningTasksOrder[i], _runningTasksOrder[i + 1]) = (_runningTasksOrder[i + 1], _runningTasksOrder[i]);
			SetTasksOrder();

			return true;
		}

		private void SetTasksOrder()
		{
			for (var i = 0; i < _runningTasksOrder.Count; i++)
				_runningTasksOrder[i].Priority = i;
		}
	}
}