using Windows.Browser.Pages;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser
{
	public class SitePanel : MonoBehaviour
	{
		[SerializeField] private Transform window;
		private Page _currentPage;
		private Text _text;

		private void Awake()
		{
			_text = GetComponentInChildren<Text>();
			GetComponentInChildren<BackButton>().SitePanel = this;
		}

		public void OpenTab(Page page)
		{
			_currentPage = Instantiate(page, window);
			gameObject.SetActive(true);
			_text.text = page.pageName;
		}

		public void CloseTab()
		{
			Destroy(_currentPage.gameObject);
			gameObject.SetActive(false);
		}
	}
}