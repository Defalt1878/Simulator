using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UserData;
using Random = UnityEngine.Random;

namespace Windows.Cmd
{
	public class ServersCrack : MonoBehaviour
	{
		private const float MinServerGenerateTime = 0.02f;
		private const float MaxServerGenerateTime = 0.5f;
		private const int MaxRequests = 40;
		private const float MinWaitTimeInSeconds = 15f;
		private const float MaxWaitTimeInSeconds = 50f;
		private const float RequestsDelay = 1.5f;
		private const float AvailableServerChance = 0.11f;

		private const int MinPackageAmount = 5;
		private const int MaxPackageAmount = 12;
		private const int MinPackageLength = 1;
		private const int MaxPackageLength = 3;
		private const float PackageDelay = 1.2f;

		private const float CrackTimeInSeconds = 7;

		public ConsoleOutput Console { get; set; }
		private HashSet<string> _availableServers;
		private static int _requestsCount;
		private static bool _serverError;

		private bool _serverConnected;
		private int _availablePackages;
		private int _packageLength;
		private string _packages;
		private int _receivedPackages;

		private void Start()
		{
			_availableServers = new HashSet<string>();
			StartCoroutine(DecreaseRequests());
		}

		public void FindServers(int count)
		{
			if (_serverConnected)
			{
				Console.Print("You can't find servers while you're connected to one!", CmdColors.Error);
				return;
			}

			StartCoroutine(FindServersCoroutine(count));
		}

		public void Connect(string ip)
		{
			if (!TryRequest())
				return;
			if (_serverConnected)
			{
				Console.Print("You're already connected to server!", CmdColors.Error);
				return;
			}

			if (_availableServers.Contains(ip))
			{
				_availableServers.Remove(ip);
				_serverConnected = true;
				InitializeServer();
				Console.Print("* Connection successful.", CmdColors.ImportantMessage);
				Console.Print($"* {_availablePackages} packages available.", CmdColors.ImportantMessage);
			}
			else
			{
				Console.Print("Not available to connect this server!", CmdColors.Error);
				Console.Print("Check correctness of ip.", CmdColors.Error);
			}
		}

		public void Disconnect()
		{
			if (!_serverConnected)
			{
				Console.Print("You're not connected to server!", CmdColors.Error);
				return;
			}

			_serverConnected = false;
			Console.Print("* Server disconnected.", CmdColors.ImportantMessage);
		}

		public void GetPackages(int count)
		{
			if (!TryRequest())
				return;
			if (!_serverConnected)
			{
				Console.Print("You're not connected to server!", CmdColors.Error);
				Console.Print("Not able to get packages!", CmdColors.Error);
			}

			Console.Print("", CmdColors.ImportantMessage);
			StartCoroutine(GetPackagesCoroutine(count));
		}

		public void TryCrack(string packages)
		{
			if (!TryRequest())
				return;
			if (!_serverConnected)
			{
				Console.Print("You're not connected to server!", CmdColors.Error);
				return;
			}

			if (packages.ToUpper() == _packages)
				StartCoroutine(Crack());
			else
			{
				Console.Print("Wrong packages! Server blocked.", CmdColors.Error);
				Disconnect();
			}
		}

		private IEnumerator FindServersCoroutine(int count)
		{
			Console.UserCanPrint = false;

			for (var i = 0; i < count; i++)
			{
				yield return new WaitForSeconds(Random.Range(MinServerGenerateTime, MaxServerGenerateTime));
				if (!TryRequest())
				{
					Console.UserCanPrint = true;
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
						? CmdColors.ImportantMessage
						: CmdColors.DefaultOutput
				);
				if (availableServer)
					_availableServers.Add(ip.ToString());
			}

			Console.UserCanPrint = true;
		}

		private void InitializeServer()
		{
			_availablePackages = Random.Range(MinPackageAmount, MaxPackageAmount + 1);
			_packageLength = Random.Range(MinPackageLength, MaxPackageLength + 1);
			_packages = GeneratePackages(_availablePackages * _packageLength);
			_receivedPackages = 0;
		}

		private IEnumerator GetPackagesCoroutine(int count)
		{
			Console.UserCanPrint = false;

			for (var i = 0; i < count; i++)
			{
				if (!TryRequest())
				{
					Console.UserCanPrint = true;
					yield break;
				}

				if (_receivedPackages == _availablePackages)
				{
					Console.Print("No more available packages!", CmdColors.Error);
					yield break;
				}

				var package = _packages.Substring(_receivedPackages++ * _packageLength, _packageLength);
				Console.ReplaceLast(package);
				yield return new WaitForSeconds(PackageDelay);
				Console.ReplaceLast("");
			}

			Console.UserCanPrint = true;
		}

		private IEnumerator Crack()
		{
			if (!TryRequest())
				yield break;
			Console.Print("Cracking started!", CmdColors.ImportantMessage);
			Console.UserCanPrint = false;
			for (var i = 10; i <= 100; i += 10)
			{
				yield return new WaitForSeconds(CrackTimeInSeconds / 9);
				Console.Print($"# {i,4}", CmdColors.DefaultOutput);
			}

			Console.Print("Crack successful!", CmdColors.ImportantMessage);
			var moneyReceived = (int) Math.Round(_packages.Length * Random.Range(1f, 4f));
			StaticData.GetInstance().Stats.Money += moneyReceived;
			Debug.Log(moneyReceived);
			Disconnect();

			Console.UserCanPrint = true;
		}

		private static string GeneratePackages(int length)
		{
			var elements = Enumerable
				.Range(0, length)
				.Select(_ => Random.Range('A', 'Z' + 1))
				.Select(Convert.ToChar);

			return string.Join("", elements);
		}

		private bool TryRequest()
		{
			_requestsCount++;
			var success =
				!_serverError &&
				(_requestsCount < MaxRequests / 2 ||
				 Random.Range(_requestsCount, MaxRequests + 1) != MaxRequests);
			if (success) return true;

			if (!_serverError)
				_requestsCount = (int) (Random.Range(
					Math.Min(_requestsCount * RequestsDelay, MinWaitTimeInSeconds),
					Math.Max(_requestsCount * RequestsDelay, MaxWaitTimeInSeconds)
				) / RequestsDelay);

			_serverError = true;

			Console.Print("Too many requests. Activity detected!", CmdColors.Error);
			Console.Print("Server error!", CmdColors.Error);
			Console.Print($"Wait for {_requestsCount * RequestsDelay,0:0} seconds", CmdColors.Error);

			return false;
		}

		private IEnumerator DecreaseRequests()
		{
			while (true)
			{
				if (_requestsCount > 0)
					_requestsCount--;
				else
					_serverError = false;
				yield return new WaitForSeconds(RequestsDelay);
			}
			// ReSharper disable once IteratorNeverReturns
		}
	}
}