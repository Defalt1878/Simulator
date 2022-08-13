namespace Windows.Cmd.Commands
{
	public interface IConsoleCommand
	{
		string Name { get; }
		string Description { get; }
		void Execute(params string[] args);
	}
}