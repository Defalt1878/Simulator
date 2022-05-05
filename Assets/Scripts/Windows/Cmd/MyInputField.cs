using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Cmd
{
	public class MyInputField : MonoBehaviour
	{
		private ConsoleOutput _consoleOutput;
		private InputField _input;
		private List<string> _lines;

		private void Awake()
		{
			_consoleOutput = GetComponentInParent<CmdWindow>().ConsoleOutput;
			_input = GetComponent<InputField>();
			_lines = new List<string>();
		}

		private void Update()
		{
			if (!_input.isFocused)
				return;
			if (Input.GetKeyDown(KeyCode.UpArrow) && TryGetPreviousInput(_input.text, out var previous))
				_input.text = previous;
			if (Input.GetKeyDown(KeyCode.DownArrow))
				_input.text = GetNextInput(_input.text);
		}

		private bool TryGetPreviousInput(string current, out string previous)
		{
			previous = _lines
				.TakeWhile(line => line != current)
				.LastOrDefault();
			return previous is not null;
		}

		private string GetNextInput(string current)
		{
			return _lines
				.SkipWhile(line => line != current)
				.Skip(1)
				.FirstOrDefault() ?? "";
		}

		public void SubmitCommand()
		{
			if (!_consoleOutput.UserCanPrint)
				return;

			_consoleOutput.HandleUserCommand(_input.text);
			_lines.Add(_input.text);
			_input.text = "";
			_input.ActivateInputField();
		}
	}
}