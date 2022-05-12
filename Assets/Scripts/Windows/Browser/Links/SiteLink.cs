using System.IO;
using Windows.Browser.Sites;
using UnityEngine;

namespace Windows.Browser.Links
{
    public abstract class SiteLink : MonoBehaviour
    {
        private Page _page;
        private SitePanel _sitePanel;
        private protected abstract string PagePath { get; }
        
        private void Awake()
        {
            _page = Resources.Load<Page>(PagePath);
            _sitePanel = GetComponentInParent<BrowserWindow>().SitePanel;
        }

        public void OnClick()
        {
            var site = Instantiate(_page, GetComponentInParent<BrowserWindow>().transform);
            _sitePanel.gameObject.SetActive(true);
            _sitePanel.CurrentPage = site.gameObject;
            _sitePanel.Name = _page.Name;
        }
    }
}
