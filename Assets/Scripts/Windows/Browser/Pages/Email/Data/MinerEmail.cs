using System;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.Email.Data
{
	[Serializable]
	public class MinerEmail : EmailData
	{
		public override string SenderName => "Unknown";
		public override string Subject => "Bitcoins";
		[NonSerialized] private protected Action<float> CheckComplete;

		public override void OnLoad()
		{
			if (IsCompleted)
				return;
			CheckComplete = hashRateValue =>
			{
				if (hashRateValue >= 150)
					OnComplete();
			};
			// StaticData.GetInstance().MiningData.OnHashRateChanged += CheckComplete;
		}

		public override void OnOpen()
		{
			var instance = StaticData.GetInstance();
			if (IsRead)
				return;
			IsRead = true;
			instance.Apps.AddToDownloads("Miner");
		}

		private void OnComplete()
		{
			var instance = StaticData.GetInstance();
			if (IsCompleted)
				return;
			IsCompleted = true;
			instance.MiningData.OnHashRateChanged -= CheckComplete;
			Debug.Log("Miner Email Completed");
			//TODO
		}

		private protected override string EmailFolder => "Miner";
	}
}