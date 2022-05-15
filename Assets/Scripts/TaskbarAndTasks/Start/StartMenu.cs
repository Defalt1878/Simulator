using UnityEngine;

namespace TaskbarAndTasks.Start
{
	public class StartMenu : MonoBehaviour
	{
		public void ExitGame()
		{
			Application.Quit();
			Debug.Log("Game exit");
		}
		
		public void SaveData()
		{
			DataSaver.SaveData();
			Debug.Log("Game saved");
		}
		
		public void ResetData()
		{
			DataSaver.ResetData();
			Debug.Log("Game reset");
		}
	}
}
