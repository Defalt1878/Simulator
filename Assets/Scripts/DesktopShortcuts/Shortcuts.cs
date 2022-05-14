using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DesktopShortcuts
{
	public class Shortcuts : MonoBehaviour
	{
		private const string ShortcutsPath = "Shortcuts";
		private List<string> _shortcuts;

		private void Awake()
		{
			_shortcuts = StaticData.GetInstance().Shortcuts;
		}

		private void Update()
		{
			if (_shortcuts.Count == transform.childCount)
				return;
			var childCount = transform.childCount;
			for (var i = 0; i < _shortcuts.Count - childCount; i++)
				InstantiateShortcut(_shortcuts[childCount + i]);
		}

		private void InstantiateShortcut(string shortcutName)
		{
			var shortcut = Resources.Load<Shortcut>(Path.Combine(ShortcutsPath, shortcutName));
			Instantiate(shortcut, transform);
		}
	}
}