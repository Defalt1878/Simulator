using Windows.Browser;
using UnityEngine;

namespace Windows.Panel
{
	public class MinimizeButton : MonoBehaviour
	{
		public void OnClick()
		{
			gameObject.GetComponentInParent<Window>().currentTask.IsMinimized = true;
		}
	}
}