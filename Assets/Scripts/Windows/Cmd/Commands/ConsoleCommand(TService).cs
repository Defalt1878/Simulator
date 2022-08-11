using System;
using System.Collections.Generic;
using Windows.Cmd.Services;

namespace Windows.Cmd.Commands
{
	public abstract class ConsoleCommand<TService> : IConsoleCommand
		where TService : ConsoleService
	{
		protected readonly TService Service;
		protected readonly Console Console;

		public abstract string Name { get; }

		protected ConsoleCommand(TService service)
		{
			Service = service;
			Console = Service.Console;
		}

		public abstract void Execute(params string[] args);

		protected bool TryParse<T>(
			IReadOnlyList<string> args,
			int expectedLength,
			out T result,
			Func<IReadOnlyList<string>, string> checker = null,
			Func<IReadOnlyList<string>, T> parser = null
		)
		{
			result = default;
			if (args.Count != expectedLength)
			{
				Console.ThrowUnknownSyntaxError();
				return false;
			}

			var errorData = checker?.Invoke(args);
			if (errorData is { })
			{
				Console.ThrowWrongDataError(errorData);
				return false;
			}

			if (parser is { })
				result = parser(args);
			return true;
		}
	}
}