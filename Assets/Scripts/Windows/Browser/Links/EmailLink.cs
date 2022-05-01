using System.IO;
using Windows.Browser.Sites;
using Windows.Browser.Sites.Email;
using UnityEngine;

namespace Windows.Browser.Links
{
	public class EmailLink : MonoBehaviour
	{
		private Site _site;

		// private GameObject _startPage;
		private SitePanel _sitePanel;
		// private BrowserWindow _browserWindow;


		private void Awake()
		{
			_site = Resources.Load<EmailPage>(Path.Combine("Windows", "Browser", "Email", "EmailPage"));
			// _startPage = GameObject.Find("StartScreen");
			_sitePanel = GetComponentInParent<BrowserWindow>().SitePanel;
			// _browserWindow = GetComponentInParent<BrowserWindow>();
		}

		public void OnClick()
		{
			var site = Instantiate(_site, GetComponentInParent<BrowserWindow>().transform);
			// _browserWindow.StartPage.SetActive(false);
			_sitePanel.gameObject.SetActive(true);
			_sitePanel.CurrentPage = site.gameObject;
			_sitePanel.Name = _site.Name;
		}
	}
}