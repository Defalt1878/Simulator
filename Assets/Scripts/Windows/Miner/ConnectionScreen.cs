using Notifications;
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
		[SerializeField] private PopUpNotification notification;

		private string _server;
		private const int TargetsAmount = 5;

		public void TryConnect()
		{
			_server = input.text;
			if (!StaticData.GetInstance().MiningData.AvailableServers.Contains(_server))
			{
				notification.Appear("Server not found!", NotificationType.Error);
				return;
			}

			connectionGame.SetActive(true);
			gameObject.SetActive(false);
			game.StartGame(TargetsAmount);
			game.ConnectionScreen = this;
			input.text = "";
		}

		public void GameFinished()
		{
			connectionGame.SetActive(false);
			gameObject.SetActive(true);
			var miningData = StaticData.GetInstance().MiningData;
			miningData.ConnectServer(_server);
			notification.Appear("Connection successful!", NotificationType.Success);
		}
	}
}