using System.IO;
using TaskbarAndTasks;
using UnityEngine;

namespace DesktopShortcuts
{
	public class BrowserShortcut : Shortcut
	{
		private void Awake()
		{
			Task = Resources.Load<Task>(Path.Combine("TaskBar", "BrowserTask"));
		}
	}
}