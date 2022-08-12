using System;
using System.Collections.Generic;
using System.Linq;

namespace UserData
{
	[Serializable]
	public class MiningData
	{
		public float BtcHashRate => 2000000f;

		public int ConnectedServersCount { get; private set; }
		public float UserHashRate { get; private set; }
		public HashSet<string> AvailableServers => _serversHashRates.Keys.ToHashSet();

		private Dictionary<string, float> _serversHashRates;
		// public List<PurchaseLotInfo> AvailableLots { get; }

		public MiningData()
		{
			// AvailableLots = new List<PurchaseLotInfo>();
			_serversHashRates = new Dictionary<string, float>();
		}

		public void NewAvailableServer(string server, float serverHashRate) =>
			_serversHashRates[server] = serverHashRate;

		public void ConnectServer(string server)
		{
			if (!_serversHashRates.Remove(server, out var serverRate))
				throw new ArgumentException();

			ConnectedServersCount++;
			UserHashRate += serverRate;
			OnNewServerConnected?.Invoke(ConnectedServersCount, UserHashRate);
		}

		[field: NonSerialized] public event Action<int, float> OnNewServerConnected;
	}
}