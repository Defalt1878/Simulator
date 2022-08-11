using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Cmd
{
	public class Console : MonoBehaviour
	{
		public bool BlockUserInput { get; set; }

		[SerializeField] private InputField consoleLine;
		private const int MaxLines = 50;
		private LinkedList<InputField> _lines;

		private void Awake()
		{
			_lines = new LinkedList<InputField>();
		}

		public void Print(string command, CmdColor color)
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

		public void ReplaceLast(string command)
		{
			var line = _lines.Last.Value;
			line.text = command;
		}

		public void Clear()
		{
			while (_lines.Any())
			{
				Destroy(_lines.First.Value.gameObject);
				_lines.RemoveFirst();
			}
		}

		public void UserPrint(string command) =>
			Print($"> {command}", CmdColor.UserInput);

		public void ThrowUnknownCommandError(string command) =>
			Print($"Unknown command: {command}", CmdColor.Error);
		
		public void ThrowUnknownSyntaxError() =>
			Print("Unknown syntax error!", CmdColor.Error);
		
		public void ThrowWrongDataError(string data) =>
			Print($"Wrong data: {data}", CmdColor.Error);
	}
}