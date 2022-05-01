using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Windows.Cmd
{
	public class ConsoleOutput : MonoBehaviour
	{
		private Text _consoleLine;

		private void Awake()
		{
			_consoleLine = Resources.Load<Text>(Path.Combine("Windows", "CmdWindow", "Line"));
		}

		public void HandleUserCommand(string command)
		{
			var consoleLine = Instantiate(_consoleLine, transform);
			consoleLine.text = $"> {command}";
			consoleLine.color = CmdColors.UserInput;
			ParseCommand(command);
		}

		private void ParseCommand(string command)
		{
			var parts = command.ToLower().Split(' ');
			switch (parts[0])
			{
				case "find":
					StartCoroutine(TryFindServers(parts));
					break;
				default:
					UnknownCommandError();
					break;
			}
		}

		private IEnumerator TryFindServers(IReadOnlyList<string> parts)
		{
			if (parts.Count != 3 || parts[1] != "servers")
				UnknownCommandError();

			if (!int.TryParse(parts[2], out var count) || count <= 0)
				WrongDataError(parts[2]);

			for (var i = 0; i < count; i++)
			{
				yield return new WaitForSeconds(Random.Range(0.02f, 0.5f));
				var ip = new StringBuilder();
				for (var j = 1; j <= 9; j++)
				{
					ip.Append(Random.Range(0, 10));
					if (j % 3 == 0 && j != 9)
						ip.Append('.');
				}

				var consoleLine = Instantiate(_consoleLine, transform);
				consoleLine.text = $"- {ip}";
				consoleLine.color = Random.Range(0, 11) == 0
					? CmdColors.ImportantMessage
					: CmdColors.DefaultOutput;
			}
		}

		private void UnknownCommandError()
		{
			var consoleLine = Instantiate(_consoleLine, transform);
			consoleLine.text = "Unknown command! Please try again.";
			consoleLine.color = CmdColors.Error;
		}

		private void WrongDataError(string data)
		{
			var consoleLine = Instantiate(_consoleLine, transform);
			consoleLine.text = $"Wrong data: \"{data}\"!";
			consoleLine.color = CmdColors.Error;
		}
	}
}