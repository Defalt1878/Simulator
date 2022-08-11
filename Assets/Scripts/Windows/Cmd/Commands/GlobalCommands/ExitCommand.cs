using Windows.Cmd.Services;
using DesktopAndShortcuts;

namespace Windows.Cmd.Commands.GlobalCommands
{
	public class ExitCommand : ConsoleCommand<GlobalService>
	{
		public ExitCommand(GlobalService service) : base(service)
		{
		}

		public override string Name => "Exit";
		public override void Execute(params string[] args)
		{
			if (!TryParse<object>(args, 1, out _))
				return;

			var task = Console.GetComponentInParent<Window>().CurrentTask;
			Desktop.Taskbar.EndTask(task);
		}
	}
}