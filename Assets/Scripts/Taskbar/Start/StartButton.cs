using UnityEngine;

namespace Taskbar.Start
{
	public class StartButton : MonoBehaviour
	{
		[SerializeField]
		private GameObject startMenu;

		public void OnClick() => startMenu.SetActive(!startMenu.activeInHierarchy);
	}
}