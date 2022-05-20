using Windows.Browser.Pages;
using UnityEngine;

namespace Windows.Browser
{
	public class PageLink : MonoBehaviour
	{
		[SerializeField] private Page page;
		[SerializeField] private SitePanel sitePanel;

		public void OnClick() => sitePanel.OpenTab(page);
	}
}