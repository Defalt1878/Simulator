using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Cmd
{
	public class InputFieldHandler : MonoBehaviour
	{
		[SerializeField] private InputField input;
		[SerializeField] private Console console;
		[SerializeField] private CommandsExecutor commandsExecutor;
		private List<string> _previousInput;
		private int _currentPosition;
		private string _currentInput;

		private void Awake()
		{
			_previousInput = new List<string>();
		}

		private void Update()
		{
			if (!input.isFocused)
				return;

			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				input.text = GetPreviousInput(input.text);
				input.caretPosition = input.text.Length;
			}

			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				input.text = GetNextInput(input.text);
				input.caretPosition = input.text.Length;
			}
		}

		private string GetPreviousInput(string current)
		{
			if (_currentPosition == 0)
				return current;
			if (_currentPosition == _previousInput.Count)
				_currentInput = current;
			return _previousInput[--_currentPosition];
		}

		private string GetNextInput(string current)
		{
			if (_currentPosition + 1 > _previousInput.Count)
				return current;
			return ++_currentPosition == _previousInput.Count
				? _currentInput
				: _previousInput[_currentPosition];
		}

		public void SubmitCommand()
		{
			if (console.BlockUserInput)
				return;

			var command = input.text;
			input.text = "";
			_previousInput.Add(command);
			_currentPosition = _previousInput.Count;
			input.ActivateInputField();

			console.UserPrint(command);
			commandsExecutor.Execute(command);
		}
	}
}