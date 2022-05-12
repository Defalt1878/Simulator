using System.IO;

namespace TaskbarAndTasks
{
	public class CmdTask : Task
	{
		private protected override string TargetWindowPath => Path.Combine("Windows", "CmdWindow");
	}
}