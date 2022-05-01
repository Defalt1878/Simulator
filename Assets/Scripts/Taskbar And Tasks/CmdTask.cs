using System.IO;
using Windows;
using UnityEngine;

namespace Taskbar_And_Tasks
{
	public class CmdTask : Task
	{
		private void Awake()
		{
			Window = Resources.Load<Window>(Path.Combine("Windows", "CmdWindow"));
		}
	}
}