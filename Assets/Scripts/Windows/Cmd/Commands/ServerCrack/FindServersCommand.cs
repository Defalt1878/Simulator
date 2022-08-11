using System;
using System.Collections;
using System.Text;
using Windows.Cmd.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Windows.Cmd.Commands.ServerCrack
{
	public class FindServersCommand : ConsoleCommand<ServerCracker>
	{
		private const float MinServerGenerateTime = 0.02f;
		private const float MaxServerGenerateTime = 0.5f;
		private const float AvailableServerChance = 0.12f;

		public override string Name => "FindServers";

		public FindServersCommand(ServerCracker cracker) : base(cracker)
		{
		}

		public override void Execute(params string[] args)
		{
			if (!TryParse(args, 2, out var count,
				    a => int.TryParse(a[1], out var cnt) && cnt > 0 ? null : a[1],
				    a => int.Parse(a[1])))
				return;

			if (Service.ServerConnected)
			{
				Console.Print("You can't find servers while you're connected to one!", CmdColor.Error);
				return;
			}

			Console.StartCoroutine(FindServersCoroutine(count));
		}

		private IEnumerator FindServersCoroutine(int count)
		{
			Console.BlockUserInput = true;

			for (var i = 0; i < count; i++)
			{
				yield return new WaitForSeconds(Random.Range(MinServerGenerateTime, MaxServerGenerateTime));
				if (!Service.TryRequest())
				{
					Console.BlockUserInput = false;
					yield break;
				}

				var ip = new StringBuilder();
				for (var j = 1; j <= 9; j++)
				{
					ip.Append(Random.Range(0, 10));
					if (j % 3 == 0 && j != 9)
						ip.Append('.');
				}

				var availableServer = Random.Range(0, (int) Math.Ceiling(1 / AvailableServerChance)) == 0;

				Console.Print(
					$"- {ip}",
					availableServer
						? CmdColor.Important
						: CmdColor.Default
				);
				if (availableServer)
					Service.AvailableServers.Add(ip.ToString());
			}

			Console.BlockUserInput = false;
		}
	}
}