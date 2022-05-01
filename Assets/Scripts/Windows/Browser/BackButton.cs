using UnityEngine;

namespace Windows.Browser
{
	public class BackButton : MonoBehaviour
	{
		private SitePanel _sitePanel;

		private void Awake()
		{
			_sitePanel = GetComponentInParent<SitePanel>();
		}

		public void OnClick() => _sitePanel.CloseTab();
	}
}