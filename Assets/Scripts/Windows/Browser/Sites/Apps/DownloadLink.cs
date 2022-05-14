using System.Collections.Generic;
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
		private HashSet<string> _availableToDownloadApps;

		private void Awake()
		{
			_availableToDownloadApps = StaticData.GetInstance().AvailableToDownloadApps;
			_icon = transform.Find("Icon").GetComponent<Image>();
			_name = transform.Find("Name").GetComponent<TextMeshProUGUI>();
			_downloadButton = transform.Find("DownloadButton").GetComponent<Button>();
			_downloadButton.GetComponent<DownloadButton>().DownloadingAppName = downloadingAppName;
			_downloadButton.interactable = _availableToDownloadApps.Contains(downloadingAppName);
			_lastAvailableAppsCount = _availableToDownloadApps.Count;
		}

		private void Update()
		{
			if (_availableToDownloadApps.Count == _lastAvailableAppsCount)
				return;
			_lastAvailableAppsCount = _availableToDownloadApps.Count;
			_downloadButton.interactable = _availableToDownloadApps.Contains(downloadingAppName);
		}
	}
}