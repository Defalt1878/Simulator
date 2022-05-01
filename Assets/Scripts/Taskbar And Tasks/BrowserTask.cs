using System.IO;

namespace Taskbar_And_Tasks
{
	public class BrowserTask : Task
	{
		private protected override string TargetWindowPath => Path.Combine("Windows", "BrowserWindow");
	}
}