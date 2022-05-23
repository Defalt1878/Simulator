using TMPro;
using UnityEngine;
using UserData;

namespace Windows.Miner
{
	public class MiningInfo : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI serversCount;
		[SerializeField] private TextMeshProUGUI userHashRate;

		private MiningData _miningData;

		private void Awake()
		{
			_miningData = StaticData.GetInstance().MiningData;
		}

		private void Update()
		{
			serversCount.text = _miningData.ConnectedServersCount.ToString();
			userHashRate.text = $"{_miningData.UserHashRate:0.##} MH/s";
		}
	}
}