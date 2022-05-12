using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DesktopShortcuts
{
	public class Shortcuts : MonoBehaviour
	{
		private static readonly List<Shortcut> _shortcuts = new();
		private const string ShortcutsPath = "Shortcuts";
		private static Transform _desktopShortcuts;

		private void Awake()
		{
			_desktopShortcuts = transform;
			NewShortcut("Browser");
		}

		public static void NewShortcut(string shortcutName)
		{
			var shortcut = Resources.Load<Shortcut>(Path.Combine(ShortcutsPath, shortcutName));
			shortcut = Instantiate(shortcut, _desktopShortcuts);
			_shortcuts.Add(shortcut);
		}
	}
}
