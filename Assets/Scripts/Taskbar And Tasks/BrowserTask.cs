using System.IO;
using Windows.Browser;
using UnityEngine;

namespace Taskbar_And_Tasks
{
	public class BrowserTask : Task
	{
		private void Awake()
		{
			Window = Resources.Load<BrowserWindow>(Path.Combine("Windows", "BrowserWindow"));
		}
	}
}