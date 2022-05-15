using UnityEngine;

namespace Windows.Browser
{
	public class BrowserWindow : Window
	{
		public override string Name => "Browser";
		[SerializeField] public SitePanel sitePanel;
	}
}