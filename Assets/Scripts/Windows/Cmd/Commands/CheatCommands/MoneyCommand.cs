using Windows.Cmd.Services;
using UserData;

namespace Windows.Cmd.Commands.CheatCommands
{
	public class MoneyCommand : ConsoleCommand<CheatService>
	{
		public MoneyCommand(CheatService service) : base(service)
		{
		}

		public override string Name => "Money";
		public override string Description => $"{Name} <amount> - add you <amount> dollars.";

		public override void Execute(params string[] args)
		{
			if (!TryParse(args, 2, out var amount,
				    a => int.TryParse(a[1], out _) ? null : a[1],
				    a => int.Parse(a[1])))
				return;

			StaticData.GetInstance().Stats.Money.Value += amount;
			Console.Print($"${amount:n} was successfully added.", CmdColor.Important);
		}
	}
}