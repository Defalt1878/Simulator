using System;
using System.Collections.Generic;

namespace UserData
{
	[Serializable]
	public class AppsData
	{
		private readonly List<string> _downloadedApps;
		private readonly HashSet<string> _availableToDownload;

		public AppsData(IEnumerable<string> downloaded, IEnumerable<string> availableToDownload)
		{
			_downloadedApps = new List<string>(downloaded);
			_availableToDownload = new HashSet<string>(availableToDownload);
		}
		
		public List<string> GetDownloaded() => _downloadedApps;

		public void Download(string app)
		{
			_availableToDownload.Remove(app);
			_downloadedApps.Add(app);
		}

		public void AddToDownloads(string app) => _availableToDownload.Add(app);

		public bool CanDownload(string app) => _availableToDownload.Contains(app);
	}
}