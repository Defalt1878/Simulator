using System.IO;

namespace Windows.Browser.Links
{
	public class AppsLink : SiteLink
	{
		private protected override string PagePath => Path.Combine("Windows", "Browser", "Apps", "AppsPage");
	}
}