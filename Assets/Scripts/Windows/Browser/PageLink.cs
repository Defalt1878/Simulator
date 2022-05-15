using Windows.Browser.Pages;
using UnityEngine;

namespace Windows.Browser
{
    public class PageLink : MonoBehaviour
    {
        [SerializeField]
        private Page page;
        private SitePanel _sitePanel;
        
        private void Awake()
        {
            _sitePanel = GetComponentInParent<BrowserWindow>().sitePanel;
        }

        public void OnClick() => _sitePanel.OpenTab(page);
    }
}
