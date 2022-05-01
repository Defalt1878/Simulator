namespace Windows.Browser
{
	public class BrowserWindow : Window
	{
		public SitePanel SitePanel { get; private set; }

		private void Awake()
		{
			SitePanel = GetComponentInChildren<SitePanel>();
			SitePanel.gameObject.SetActive(false);
		}
	}
}