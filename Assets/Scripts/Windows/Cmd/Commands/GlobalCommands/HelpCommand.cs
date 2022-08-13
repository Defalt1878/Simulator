using System.Linq;
using Windows.Cmd.Services;

namespace Windows.Cmd.Commands.GlobalCommands
{
	public class HelpCommand : ConsoleCommand<GlobalService>
	{
		public HelpCommand(GlobalService service) : base(service)
		{
		}

		public override string Name => "Help";
		public override string Description => $"{Name} <command> - give info about specific command.";

		public override void Execute(params string[] args)
		{
			switch (args.Length)
			{
				case 1:
					PrintAllCommands();
					return;
				case 2:
					PrintSpecificCommandInfo(args[1]);
					return;
				default:
					Console.ThrowUnknownSyntaxError();
					return;
			}
		}

		private void PrintSpecificCommandInfo(string command)
		{
			var cmd = Service.GetCommandByName(command);
			if (cmd is null)
				Console.Print($"Command not found: {command}", CmdColor.Error);
			else
				Console.Print(cmd.Description, CmdColor.Default);
		}

		private void PrintAllCommands()
		{
			Console.Print("Available commands:", CmdColor.Important);
			foreach (var command in Service.GetAllCommands().Select(cmd => cmd.Name))
				Console.Print(command, CmdColor.Default);
		}
	}
}