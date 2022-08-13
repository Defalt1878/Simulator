using System.Collections;
using Windows.Cmd.Services;
using UnityEngine;

namespace Windows.Cmd.Commands.ServerCrack
{
	public class GetPackagesCommand : ConsoleCommand<ServerCracker>
	{
		private const float PackageDelay = 1.2f;
		private const float BlinkDelay = 0.3f;

		public override string Name => "GetPackages";
		public override string Description => $"{Name} <count> - receive <count> packages from current server.";

		public GetPackagesCommand(ServerCracker cracker) : base(cracker)
		{
		}

		public override void Execute(params string[] args)
		{
			if (!TryParse(args, 2, out var count,
				    a => int.TryParse(a[1], out var cnt) && cnt > 0 ? null : a[1],
				    a => int.Parse(a[1])))
				return;

			if (!Service.TryRequest())
				return;

			if (!Service.ServerConnected)
			{
				Console.Print("You're not connected to server!", CmdColor.Error);
				Console.Print("Not able to get packages!", CmdColor.Error);
			}

			Console.Print("", CmdColor.Important);
			Console.StartCoroutine(GetPackagesCoroutine(count));
		}
		
		private IEnumerator GetPackagesCoroutine(int count)
		{
			Console.BlockUserInput = true;

			for (var i = 0; i < count; i++)
			{
				if (!Service.TryRequest())
				{
					Console.BlockUserInput = false;
					yield break;
				}

				if (Service.ReceivedPackages == Service.AvailablePackages)
				{
					Console.Print("No more available packages!", CmdColor.Error);
					Console.BlockUserInput = false;
					yield break;
				}

				var package = Service.Packages
					.Substring(Service.ReceivedPackages++ * Service.PackageLength, Service.PackageLength);
				Console.ReplaceLast(package);
				yield return new WaitForSeconds(PackageDelay);
				Console.ReplaceLast("");
				yield return new WaitForSeconds(BlinkDelay);
			}

			Console.BlockUserInput = false;
		}
	}
}