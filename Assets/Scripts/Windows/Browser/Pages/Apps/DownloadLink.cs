using DesktopShortcuts;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.Apps
{
	public class DownloadLink : MonoBehaviour
	{
		[SerializeField] private Shortcut downloadingAppShortcut;
		private string _downloadingAppName;

		private DownloadButton _downloadButton;

		private void Awake()
		{
			_downloadingAppName = downloadingAppShortcut.name;
			_downloadButton = transform.Find("DownloadButton").GetComponent<DownloadButton>();
			_downloadButton.DownloadingAppName = _downloadingAppName;
		}

		private void Update()
		{
			_downloadButton.Active = 
				StaticData.GetInstance().Apps.CanDownload(_downloadingAppName) &&
				!_downloadButton.Downloading;
		}
	}
}