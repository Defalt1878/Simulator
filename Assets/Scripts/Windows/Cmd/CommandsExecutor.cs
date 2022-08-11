using Windows.Cmd.Services;
using UnityEngine;

namespace Windows.Cmd
{
	public class CommandsExecutor : MonoBehaviour
	{
		[SerializeField] private Console console;

		private GlobalService _globalService;

		private void Awake()
		{
			_globalService = new GlobalService(console);
		}

		public void Execute(string command)
		{
			var args = command.Split(' ');
			var cmd = _globalService.GetCommandByName(args[0]);
			if (cmd is null)
				console.ThrowUnknownCommandError(args[0]);
			else
				cmd.Execute(args);
		}
	}
}