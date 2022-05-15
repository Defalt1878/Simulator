using TMPro;
using UnityEngine;

namespace Windows.Panel
{
	public class WindowName : MonoBehaviour
	{
		private void Awake()
		{
			var text = GetComponent<TextMeshProUGUI>();
			text.text = GetComponentInParent<Window>().Name;
		}
	}
}