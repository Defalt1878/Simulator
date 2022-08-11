using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Windows.Cmd.Services
{
	public class ServerCracker : ConsoleService
	{
		public override string Name => "Cracker";

		private const int MaxRequests = 40;
		private const float MinWaitTimeInSeconds = 15f;
		private const float MaxWaitTimeInSeconds = 50f;
		private const float RequestsDelay = 1.5f;

		private static int _requestsCount;
		private static bool _serverError;

		public HashSet<string> AvailableServers { get; }
		public bool ServerConnected { get; set; }
		public int AvailablePackages { get; set; }
		public int PackageLength { get; set; }
		public string Packages { get; set; }
		public int ReceivedPackages { get; set; }

		public ServerCracker(Console console) : base(console)
		{
			AvailableServers = new HashSet<string>();
			Console.StartCoroutine(DecreaseRequests());
		}

		public bool TryRequest()
		{
			_requestsCount++;
			var success =
				!_serverError &&
				(_requestsCount < MaxRequests / 2 || Random.Range(_requestsCount, MaxRequests + 1) != MaxRequests);
			if (success) return true;

			if (!_serverError)
				_requestsCount = (int) (Random.Range(
					Math.Min(_requestsCount * RequestsDelay, MinWaitTimeInSeconds),
					Math.Max(_requestsCount * RequestsDelay, MaxWaitTimeInSeconds)
				) / RequestsDelay);

			_serverError = true;

			Console.Print("Too many requests. Activity detected!", CmdColor.Error);
			Console.Print("Server error!", CmdColor.Error);
			Console.Print($"Wait for {_requestsCount * RequestsDelay,0:0} seconds", CmdColor.Error);

			return false;
		}

		private static IEnumerator DecreaseRequests()
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