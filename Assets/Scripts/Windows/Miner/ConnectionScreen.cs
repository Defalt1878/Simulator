using TMPro;
using UnityEngine;
using UserData;

namespace Windows.Miner
{
	public class ConnectionScreen : MonoBehaviour
	{
		[SerializeField] private TMP_InputField input;
		[SerializeField] private GameObject connectionGame;
		[SerializeField] private GameField game;
		private string _server;
		public void TryConnect()
		{
			_server = input.text;
			if (!StaticData.GetInstance().MiningData.ServersHashRates.ContainsKey(_server))
			{
				Debug.LogWarning("Server not found!");
				return;
			}

			connectionGame.SetActive(true);
			gameObject.SetActive(false);
			game.StartGame(5);
			game.ConnectionScreen = this;
			input.text = "";
		}

		public void GameFinished()
		{
			connectionGame.SetActive(false);
			gameObject.SetActive(true);
			var miningData = StaticData.GetInstance().MiningData;
			var hashRate = miningData.ServersHashRates[_server];
			miningData.ServersHashRates.Remove(_server);
			miningData.UserHashRate += hashRate;
			miningData.ConnectedServersCount++;
		}
	}
}