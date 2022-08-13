using System;
using System.Linq;
using Windows.Cmd.Services;
using Random = UnityEngine.Random;

namespace Windows.Cmd.Commands.ServerCrack
{
	public class ConnectCommand : ConsoleCommand<ServerCracker>
	{
		private const int MinPackageAmount = 5;
		private const int MaxPackageAmount = 12;
		private const int MinPackageLength = 1;
		private const int MaxPackageLength = 3;


		public override string Name => "Connect";
		public override string Description => $"{Name} <ip> - connect to a server.";

		public ConnectCommand(ServerCracker cracker) : base(cracker)
		{
		}

		public override void Execute(params string[] args)
		{
			if (!TryParse(args, 2, out var ip,
				    a => CheckIp(a[1]) ? null : a[1],
				    a => a[1]))
				return;

			if (!Service.TryRequest())
				return;

			if (Service.ServerConnected)
			{
				Console.Print("You're already connected to server!", CmdColor.Error);
				return;
			}

			if (Service.AvailableServers.Contains(ip))
			{
				Service.AvailableServers.Remove(ip);
				Service.ServerConnected = true;
				InitializeServer();
				Console.Print("* Connection successful.", CmdColor.Important);
				Console.Print($"* {Service.AvailablePackages} packages available.", CmdColor.Important);
			}
			else
			{
				Console.Print("Not available to connect this server!", CmdColor.Error);
				Console.Print("Check correctness of ip.", CmdColor.Error);
			}
		}

		private void InitializeServer()
		{
			Service.AvailablePackages = Random.Range(MinPackageAmount, MaxPackageAmount + 1);
			Service.PackageLength = Random.Range(MinPackageLength, MaxPackageLength + 1);
			Service.Packages = GeneratePackages(Service.AvailablePackages * Service.PackageLength);
			Service.ReceivedPackages = 0;
		}

		private static string GeneratePackages(int length)
		{
			var elements = Enumerable
				.Range(0, length)
				.Select(_ => Random.Range('A', 'Z' + 1))
				.Select(Convert.ToChar);

			return string.Join("", elements);
		}

		private static bool CheckIp(string ip)
		{
			if (ip.Length != 11)
				return false;
			var digitsCount = 0;
			foreach (var symbol in ip)
			{
				if (digitsCount < 3)
				{
					if (!char.IsDigit(symbol))
						return false;
					digitsCount++;
				}
				else
				{
					if (symbol != '.')
						return false;
					digitsCount = 0;
				}
			}

			return true;
		}
	}
}