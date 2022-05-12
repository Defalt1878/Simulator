using System.IO;

namespace TaskbarAndTasks
{
	public class BrowserTask : Task
	{
		private protected override string TargetWindowPath => Path.Combine("Windows", "BrowserWindow");
	}
}