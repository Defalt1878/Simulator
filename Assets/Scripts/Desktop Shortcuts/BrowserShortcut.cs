using System;
using System.IO;
using Taskbar_And_Tasks;
using UnityEngine;

namespace Desktop_Shortcuts
{
	public class BrowserShortcut : Shortcut
	{
		private void Awake()
		{
			Task = Resources.Load<Task>(Path.Combine("TaskBar", "BrowserTask"));
		}
	}
}