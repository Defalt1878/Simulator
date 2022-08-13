using UnityEngine;
using UserData;

namespace DesktopAndShortcuts
{
	public class Shortcuts : MonoBehaviour
	{
		private void Update()
		{
			var shortcuts = StaticData.GetInstance().Apps.GetDownloaded();
			if (shortcuts.Count == transform.childCount)
				return;
			if (shortcuts.Count < transform.childCount)
				foreach (Transform child in transform)
					Destroy(child.gameObject);

			var childCount = transform.childCount;
			for (var i = 0; i < shortcuts.Count - childCount; i++)
				Instantiate(shortcuts[childCount + i].LoadShortcut(), transform);
		}
	}
}