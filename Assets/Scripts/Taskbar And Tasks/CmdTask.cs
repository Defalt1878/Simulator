using System.IO;

namespace Taskbar_And_Tasks
{
	public class CmdTask : Task
	{
		private protected override string TargetWindowPath => Path.Combine("Windows", "CmdWindow");
	}
}