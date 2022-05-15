using TaskbarAndTasks;
using UnityEngine;

namespace Windows
{
	public abstract class Window : MonoBehaviour
	{
		public Task CurrentTask { get; set; }
		public abstract string Name { get; }
	}
}