using UnityEngine;

namespace Windows.Browser
{
	public class BrowserWindow : Window
	{
		// public GameObject StartPage { get; private set; }
		public SitePanel SitePanel { get; private set; }

		private void Awake()
		{
			SitePanel = GetComponentInChildren<SitePanel>();
			SitePanel.gameObject.SetActive(false);
			// SitePanel.StartScreen = GameObject.Find("StartScreen");
			// StartPage = GameObject.Find("StartScreen");
		}
	}
}