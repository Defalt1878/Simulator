using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser.Sites.Apps
{
	public class DownloadLink : MonoBehaviour
	{
		[SerializeField] private string downloadingAppName;

		private Image _icon;
		private TextMeshProUGUI _name;
		private Button _downloadButton;
		private int _lastAvailableAppsCount;

		private void Awake()
		{
			_icon = transform.Find("Icon").GetComponent<Image>();
			_name = transform.Find("Name").GetComponent<TextMeshProUGUI>();
			_downloadButton = transform.Find("DownloadButton").GetComponent<Button>();
			_downloadButton.GetComponent<DownloadButton>().DownloadingAppName = downloadingAppName;
			_downloadButton.interactable = AppsPage.AvailableToDownloadApps.Contains(downloadingAppName);
			_lastAvailableAppsCount = AppsPage.AvailableToDownloadApps.Count;
		}

		private void Update()
		{
			if (AppsPage.AvailableToDownloadApps.Count == _lastAvailableAppsCount)
				return;
			_lastAvailableAppsCount = AppsPage.AvailableToDownloadApps.Count;
			_downloadButton.interactable = AppsPage.AvailableToDownloadApps.Contains(downloadingAppName);
		}
	}
}