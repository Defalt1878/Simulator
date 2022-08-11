using Windows.Cmd.Services;
using UserData;

namespace Windows.Cmd.Commands.GlobalCommands
{
	public class MoneyCommand : ConsoleCommand<GlobalService>
	{
		public MoneyCommand(GlobalService service) : base(service)
		{
		}

		public override string Name => "Money";

		public override void Execute(params string[] args)
		{
			if (!TryParse(args, 3, out var amount,
				    a => int.TryParse(a[1], out _) ? a[2] != "1878" ? a[2] : null : a[1],
				    a => int.Parse(a[1])))
				return;

			StaticData.GetInstance().Stats.Money += amount;
			Console.Print($"${amount:n} was successfully added.", CmdColor.Important);
		}
	}
}