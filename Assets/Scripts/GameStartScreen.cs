using System.Threading.Tasks;
using UnityEngine;
using UserData;

public class GameStartScreen : MonoBehaviour
{
	private Task _loadingTask;
	private const float LoadingScreenMinTime = 2f;

	private void Start()
	{
		_loadingTask = Task.Run(StaticData.GetInstance);
		Invoke(nameof(StartGame), LoadingScreenMinTime);
	}

	private void StartGame()
	{
		if (!_loadingTask.IsCompleted)
		{
			Invoke(nameof(StartGame), 0.5f);
			return;
		}

		gameObject.SetActive(false);
	}
}