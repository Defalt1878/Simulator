using Windows.Browser.Pages;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser
{
	public class SitePanel : MonoBehaviour
	{
		[SerializeField] private Transform window;
		[SerializeField] private LoadingScreen loadingScreen;
		private Page _currentPage;
		private Text _text;

		private void Awake()
		{
			_text = GetComponentInChildren<Text>();
			GetComponentInChildren<BackButton>().SitePanel = this;
		}

		public void OpenTab(Page page)
		{
			gameObject.SetActive(true);
			_text.text = page.pageName;
			loadingScreen.OnLoadEnd += () => _currentPage = Instantiate(page, window);
			loadingScreen.StartLoading();
		}

		public void CloseTab()
		{
			gameObject.SetActive(false);
			Destroy(_currentPage.gameObject);
			loadingScreen.StartLoading();
		}
	}
}