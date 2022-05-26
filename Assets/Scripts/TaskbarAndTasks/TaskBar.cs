using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TaskbarAndTasks
{
	public class TaskBar : MonoBehaviour
	{
		private List<Task> _runningTasks;
		private List<Task> _runningTasksOrder;

		private void Awake()
		{
			_runningTasks = new List<Task>();
			_runningTasksOrder = new List<Task>();
		}

		public Task AddOrExpandTask(Task taskPrefab, Task instTask)
		{
			if (_runningTasks.Contains(instTask))
			{
				instTask.OnClick();
				return instTask;
			}

			instTask = Instantiate(taskPrefab, transform);
			_runningTasks.Add(instTask);
			_runningTasksOrder.Add(instTask);
			instTask.Priority = _runningTasksOrder.Count;
			return instTask;
		}

		public void EndTask(Task task)
		{
			if (!_runningTasks.Contains(task))
				return;

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
					(_runningTasksOrder[i], _runningTasksOrder[i + 1]) =
						(_runningTasksOrder[i + 1], _runningTasksOrder[i]);
			SetTasksOrder();

			return true;
		}

		private void SetTasksOrder()
		{
			for (var i = 0; i < _runningTasksOrder.Count; i++)
				_runningTasksOrder[i].Priority = i + 1;
		}
	}
}