using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesktopAndShortcuts;
using UnityEngine;

namespace UserData
{
	[Serializable]
	public class App
	{
		[NonSerialized] private static readonly HashSet<string> AvailableShortcuts;

		static App()
		{
			AvailableShortcuts = Resources.LoadAll<Shortcut>(ShortcutsPath)
				.Select(shortcut => shortcut.name)
				.ToHashSet();
		}

		public static App Browser => new("Browser");
		public static App Stats => new("Stats");
		public static App Cmd => new("Cmd");
		public static App Miner => new("Miner");

		public static App Parse(string name) =>
			new(name);

		private const string ShortcutsPath = "Shortcuts";

		public string Name { get; }

		private App(string name)
		{
			if (!AvailableShortcuts.Contains(name))
				throw new ArgumentException(nameof(name));
			Name = name;
		}

		public Shortcut LoadShortcut() =>
			Resources.Load<Shortcut>(Path.Combine(ShortcutsPath, Name));

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((App) obj);
		}

		public bool Equals(App other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Name == other.Name;
		}

		public override int GetHashCode()
		{
			return Name != null ? Name.GetHashCode() : 0;
		}
	}
}