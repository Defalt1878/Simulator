﻿namespace Windows.Cmd.Commands
{
	public interface IConsoleCommand
	{
		string Name { get; }
		void Execute(params string[] args);
	}
}