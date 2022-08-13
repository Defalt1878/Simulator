using System;
using System.Collections.Generic;

namespace UserData
{
	[Serializable]
	public class AppsData
	{
		private readonly List<App> _downloadedApps;
		private readonly HashSet<App> _availableToDownload;

		public AppsData(IEnumerable<App> downloaded, IEnumerable<App> availableToDownload)
		{
			_downloadedApps = new List<App>(downloaded);
			_availableToDownload = new HashSet<App>(availableToDownload);
		}

		public IReadOnlyList<App> GetDownloaded() => _downloadedApps;

		public void Download(App app)
		{
			_availableToDownload.Remove(app);
			_downloadedApps.Add(app);
		}

		public void AddToDownloads(App app) => _availableToDownload.Add(app);

		public bool CanDownload(App app) => _availableToDownload.Contains(app);
	}
}