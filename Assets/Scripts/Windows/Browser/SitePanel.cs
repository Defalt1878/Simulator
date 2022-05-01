using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser
{
	public class SitePanel : MonoBehaviour
	{
		public GameObject CurrentPage { get; set; }
		// private BrowserWindow _browserWindow;

		public string Name
		{
			get => _text.text;
			set => _text.text = value;
		}

		// private BackButton _backButton;
		private Text _text;

		private void Awake()
		{
			// _backButton = GetComponentInChildren<BackButton>();
			_text = GetComponentInChildren<Text>();
			// _browserWindow = GetComponentInParent<BrowserWindow>();
		}

		public void CloseTab()
		{
			// _browserWindow.StartPage.SetActive(true);
			Destroy(CurrentPage);
			gameObject.SetActive(false);
		}
	}
}