using System.Collections.Generic;

namespace Windows.Browser.Sites.Apps
{
	public class AppsPage : Page
	{
		public override string Name => "Apps Download";
		public static readonly HashSet<string> AvailableToDownloadApps = new()
		{
			"CMD"
		};

		void Awake()
		{
		}
	}
}