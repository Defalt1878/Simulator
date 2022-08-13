using Windows.Cmd.Services;

namespace Windows.Cmd.Commands.GlobalCommands
{
	public class RunServiceCommand : ConsoleCommand<GlobalService>
	{
		public RunServiceCommand(GlobalService service) : base(service)
		{
		}

		public override string Name => "Run";
		public override string Description => $"{Name} <service> - start service.";

		public override void Execute(params string[] args)
		{
			if (!TryParse<object>(args, 2, out _))
				return;

			if (Service.CurrentService is not null)
			{
				Console.Print("Any server is already started!", CmdColor.Error);
				return;
			}

			if (Service.TryRunService(args[1]))
				Console.Print($"Service {Service.CurrentService.Name} successfully started.", CmdColor.Default);
			else
				Console.Print("Service not found!", CmdColor.Error);
		}
	}
}