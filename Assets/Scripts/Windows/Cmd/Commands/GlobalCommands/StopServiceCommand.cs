using Windows.Cmd.Services;

namespace Windows.Cmd.Commands.GlobalCommands
{
	public class StopServiceCommand : ConsoleCommand<GlobalService>
	{
		public StopServiceCommand(GlobalService service) : base(service)
		{
		}

		public override string Name => "StopService";
		public override void Execute(params string[] args)
		{
			if(!TryParse<object>(args, 1, out _))
				return;
			if (Service.CurrentService is null)
			{
				Console.Print("No service started!", CmdColor.Error);
				return;
			}

			var serviceName = Service.CurrentService.Name;
			Service.StopService();
			Console.Print($"Service {serviceName} successfully stopped.", CmdColor.Default);
		}
	}
}