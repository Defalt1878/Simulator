using Windows.Cmd.Services;

namespace Windows.Cmd.Commands.ServerCrack
{
	public class DisconnectCommand : ConsoleCommand<ServerCracker>
	{
		public DisconnectCommand(ServerCracker cracker) : base(cracker)
		{
		}

		public override string Name => "Disconnect";

		public override void Execute(params string[] args)
		{
			if (!TryParse<object>(args, 1, out _))
				return;
			
			if (!Service.ServerConnected)
			{
				Console.Print("You're not connected to server!", CmdColor.Error);
				return;
			}

			Service.ServerConnected = false;
			Console.Print("* Server disconnected.", CmdColor.Important);
		}
	}
}