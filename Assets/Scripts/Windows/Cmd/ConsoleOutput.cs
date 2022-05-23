using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UserData;

namespace Windows.Cmd
{
	public class ConsoleOutput : MonoBehaviour
	{
		public bool UserCanPrint { get; set; }

		private const int MaxLines = 50;

		[SerializeField] private InputField consoleLine;

		private LinkedList<InputField> _lines;
		private ServersCrack _serversCrack;

		private void Awake()
		{
			_lines = new LinkedList<InputField>();
			_serversCrack = gameObject.AddComponent<ServersCrack>();
			_serversCrack.Console = this;
			UserCanPrint = true;
		}

		public void HandleUserCommand(string command)
		{
			Print($"> {command}", CmdColors.UserInput);
			ParseCommand(command);
		}

		internal void Print(string command, Color color)
		{
			var instLine = Instantiate(consoleLine, transform);
			instLine.text = command;
			instLine.GetComponentInChildren<Text>().color = color;
			_lines.AddLast(instLine);
			if (_lines.Count <= MaxLines)
				return;
			Destroy(_lines.First.Value.gameObject);
			_lines.RemoveFirst();
		}

		internal void ReplaceLast(string command)
		{
			var line = _lines.Last.Value;
			line.text = command;
		}

		private void ParseCommand(string command)
		{
			var parts = command.ToLower().Split(' ');
			switch (parts[0])
			{
				case "find":
				{
					if (parts.Length != 3 || parts[1] != "servers")
						goto default;
					if (!int.TryParse(parts[2], out var count) || count <= 0)
					{
						WrongDataError(parts[2]);
						return;
					}

					_serversCrack.FindServers(count);
					break;
				}

				case "connect":
				{
					if (parts.Length != 2)
						goto default;
					if (parts[1].Count(char.IsNumber) != 9)
					{
						WrongDataError(parts[2]);
						return;
					}

					_serversCrack.Connect(parts[1]);
					break;
				}

				case "disconnect":
				{
					_serversCrack.Disconnect();
					break;
				}

				case "get":
				{
					if (parts.Length != 3 || parts[1] != "packages")
						goto default;
					if (!int.TryParse(parts[2], out var count) || count <= 0)
					{
						WrongDataError(parts[2]);
						return;
					}

					_serversCrack.GetPackages(count);
					break;
				}

				case "crack":
				{
					if (parts.Length != 2)
						goto default;
					_serversCrack.TryCrack(parts[1]);
					break;
				}

				case "money":
				{
					if (parts.Length != 2)
						goto default;
					if (!int.TryParse(parts[1], out var amount))
					{
						WrongDataError(parts[1]);
						return;
					}

					StaticData.GetInstance().Stats.Money += amount;
					break;
				}

				default:
					UnknownCommandError();
					break;
			}
		}

		private void UnknownCommandError() => Print("Unknown command! Please try again.", CmdColors.Error);
		private void WrongDataError(string data) => Print($"Wrong data: \"{data}\"!", CmdColors.Error);
	}
}