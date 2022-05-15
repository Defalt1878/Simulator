using UnityEngine;

namespace Windows.Browser
{
	public class BackButton : MonoBehaviour
	{
		public SitePanel SitePanel { get; set; }

		public void OnClick() => SitePanel.CloseTab();
	}
}