using DesktopAndShortcuts;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.Apps
{
	public class DownloadLink : MonoBehaviour
	{
		[SerializeField] private Shortcut downloadingAppShortcut;
		[SerializeField] private Downloader downloader;
		private App _downloadingApp;
		private AppsData _apps;

		private void Awake()
		{
			_downloadingApp = downloadingAppShortcut.App;
			_apps = StaticData.GetInstance().Apps;
		}

		private void Update()
		{
			downloader.Active = _apps.CanDownload(_downloadingApp) && !downloader.Downloading;
		}
 
		public void Download() =>
			downloader.StartDownload(_downloadingApp);
	}
}