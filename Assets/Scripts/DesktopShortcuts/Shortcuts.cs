using System.IO;
using UnityEngine;
using UserData;

namespace DesktopShortcuts
{
	public class Shortcuts : MonoBehaviour
	{
		private const string ShortcutsPath = "Shortcuts";

		private void Update()
		{
			var shortcuts = StaticData.GetInstance().Shortcuts;
			if (shortcuts.Count == transform.childCount)
				return;
			if (shortcuts.Count < transform.childCount)
				foreach (Transform child in transform)
					Destroy(child.gameObject);

			var childCount = transform.childCount;
			for (var i = 0; i < shortcuts.Count - childCount; i++)
				InstantiateShortcut(shortcuts[childCount + i]);
		}

		private void InstantiateShortcut(string shortcutName)
		{
			var shortcut = Resources.Load<Shortcut>(Path.Combine(ShortcutsPath, shortcutName));
			Instantiate(shortcut, transform);
		}
	}
}