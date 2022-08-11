using System;
using System.Collections;
using Windows.Cmd.Services;
using JetBrains.Annotations;
using UnityEngine;
using UserData;
using Random = UnityEngine.Random;

namespace Windows.Cmd.Commands.ServerCrack
{
	public class CrackCommand : ConsoleCommand<ServerCracker>
	{
		private const float CrackTimeInSeconds = 7;

		public override string Name => "Crack";

		private readonly Action _disconnect;

		public CrackCommand(ServerCracker service) : base(service)
		{
			_disconnect = () => Service.GetCommandByName("Disconnect").Execute("Disconnect");
		}

		public override void Execute(params string[] args)
		{
			if (!TryParse(args, 2, out var packages, parser: a => a[1]))
				return;

			if (!Service.TryRequest())
				return;
			if (!Service.ServerConnected)
			{
				Console.Print("You're not connected to server!", CmdColor.Error);
				return;
			}

			if (packages.ToUpper() == Service.Packages)
				Console.StartCoroutine(Crack());
			else
			{
				Console.Print("Wrong packages! Server blocked.", CmdColor.Error);
				_disconnect();
			}
		}

		private IEnumerator Crack()
		{
			if (!Service.TryRequest())
				yield break;
			Console.Print("Cracking started!", CmdColor.Important);
			Console.BlockUserInput = true;
			for (var i = 10; i <= 100; i += 10)
			{
				yield return new WaitForSeconds(CrackTimeInSeconds / 9);
				Console.Print($"# {i,4}", CmdColor.Default);
			}

			Console.Print("Crack successful!", CmdColor.Important);
			var moneyReceived = (int) Math.Round(Service.Packages.Length * Random.Range(1f, 4f));
			StaticData.GetInstance().Stats.Money += moneyReceived;
			Debug.Log(moneyReceived);
			_disconnect();

			Console.BlockUserInput = false;
		}
	}
}