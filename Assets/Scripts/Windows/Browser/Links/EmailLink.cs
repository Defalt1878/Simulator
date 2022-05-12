using System.IO;

namespace Windows.Browser.Links
{
	public class EmailLink : SiteLink
	{
		private protected override string PagePath => Path.Combine("Windows", "Browser", "Email", "EmailPage");
	}
}