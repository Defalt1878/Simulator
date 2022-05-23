using UnityEngine;
using UserData;

namespace Taskbar.Start
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
