using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Cmd.Commands;

namespace Windows.Cmd.Services
{
	public abstract class ConsoleService
	{
		public abstract string Name { get; }
		public Console Console { get; }
		private readonly Dictionary<string, IConsoleCommand> _commands;

		protected ConsoleService(Console console)
		{
			Console = console;

			var specificCommandsBase = typeof(ConsoleCommand<>).MakeGenericType(GetType());
			_commands = specificCommandsBase.Assembly.ExportedTypes
				.Where(specificCommandsBase.IsAssignableFrom)
				.Select(t => (IConsoleCommand) Activator.CreateInstance(t, this))
				.ToDictionary(cmd => cmd.Name.ToLower(), cmd => cmd);
		}

		public virtual IConsoleCommand GetCommandByName(string name) =>
			_commands.ContainsKey(name.ToLower()) ? _commands[name.ToLower()] : null;
	}
}