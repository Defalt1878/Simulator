using System;
using Windows.Cmd.Services;
using UserData;

namespace Windows.Cmd.Commands.CheatCommands
{
	public class SetTimeCommand : ConsoleCommand<CheatService>
	{
		public SetTimeCommand(CheatService service) : base(service)
		{
		}

		public override string Name => "AddTime";
		public override string Description => $"{Name} <minutes> - add <minutes> to current time.";

		public override void Execute(params string[] args)
		{
			if (!TryParse(args, 2, out var minutes,
				    a => double.TryParse(a[1], out _) ? null : a[1],
				    a => TimeSpan.FromMinutes(double.Parse(a[1]))
			    ))
				return;

			StaticData.GetInstance().CurrentInGameTime += minutes;
			Console.Print($"{minutes} was successfully added.", CmdColor.Default);
		}
	}
}