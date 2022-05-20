using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Cmd
{
	public class InputFieldHandler : MonoBehaviour
	{
		[SerializeField] private ConsoleOutput consoleOutput;
		private InputField _input;
		private List<string> _lines;
		private int _currentPosition;
		private string _savedInput;

		private void Awake()
		{
			_input = GetComponent<InputField>();
			_lines = new List<string>();
		}

		private void Update()
		{
			if (!_input.isFocused)
				return;
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				_input.text = GetPreviousInput(_input.text);
				_input.caretPosition = _input.text.Length;
			}

			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				_input.text = GetNextInput(_input.text);
				_input.caretPosition = _input.text.Length;
			}
		}

		private string GetPreviousInput(string current)
		{
			if (_currentPosition == 0)
				return current;
			if (_currentPosition == _lines.Count)
				_savedInput = current;
			return _lines[--_currentPosition];
		}

		private string GetNextInput(string current)
		{
			if (_currentPosition + 1 > _lines.Count)
				return current;
			return ++_currentPosition == _lines.Count
				? _savedInput
				: _lines[_currentPosition];
		}

		public void SubmitCommand()
		{
			if (!consoleOutput.UserCanPrint)
				return;

			consoleOutput.HandleUserCommand(_input.text);
			_lines.Add(_input.text);
			_input.text = "";
			_input.ActivateInputField();
			_currentPosition = _lines.Count;
		}
	}
}