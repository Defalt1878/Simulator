using System.IO;
using UnityEngine;

public class BrowserTask : Task
{
	private void Awake()
	{
		Window = Resources.Load<BrowserWindow>(Path.Combine("Windows", "BrowserWindow"));
	}
}