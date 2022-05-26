using System;
using System.Collections.Generic;
using Windows.Browser.Pages.DarkMarket;

namespace UserData
{
	[Serializable]
	public class MiningData
	{
		public float BtcHashRate => 2000000f;
		private float _userHashRate;
		public int ConnectedServersCount { get; set; }

		public float UserHashRate
		{
			get => _userHashRate;
			set
			{
				_userHashRate = value;
				OnHashRateChanged?.Invoke(_userHashRate);
			}
		}

		public List<PurchaseLotInfo> AvailableLots { get; }
		public Dictionary<string, float> ServersHashRates { get; }

		public MiningData()
		{
			AvailableLots = new List<PurchaseLotInfo>();
			ServersHashRates = new Dictionary<string, float>();
		}

		[field: NonSerialized] public event Action<float> OnHashRateChanged;
	}
}