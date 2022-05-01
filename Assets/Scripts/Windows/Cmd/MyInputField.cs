using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Windows.Cmd
{
	public class MyInputField : MonoBehaviour
	{
		private ConsoleOutput _consoleOutput;
		private InputField _input;

		private void Awake()
		{
			_consoleOutput = GetComponentInParent<CmdWindow>().ConsoleOutput;
			_input = GetComponent<InputField>();
		}

		public void SubmitCommand()
		{
			_consoleOutput.HandleUserCommand(_input.text);
			_input.text = "";
		}
	}
}