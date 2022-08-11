using Windows.Cmd.Services;

namespace Windows.Cmd.Commands.GlobalCommands
{
	public class ClearCommand : ConsoleCommand<GlobalService>
	{
		public ClearCommand(GlobalService service) : base(service)
		{
		}

		public override string Name => "Clear";
		
		public override void Execute(params string[] args)
		{
			if (!TryParse<object>(args, 1, out _))
				return;
			
			Console.Clear();
		}
	}
}