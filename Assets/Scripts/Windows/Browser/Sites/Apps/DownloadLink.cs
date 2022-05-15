using UnityEngine;

namespace Windows.Browser.Sites.Apps
{
	public class DownloadLink : MonoBehaviour
	{
		[SerializeField] private string downloadingAppName;

		private DownloadButton _downloadButton;

		private void Awake()
		{
			_downloadButton = transform.Find("DownloadButton").GetComponent<DownloadButton>();
			_downloadButton.DownloadingAppName = downloadingAppName;
		}

		private void Update()
		{
			_downloadButton.Active = StaticData.GetInstance().AvailableToDownloadApps.Contains(downloadingAppName);
		}
	}
}