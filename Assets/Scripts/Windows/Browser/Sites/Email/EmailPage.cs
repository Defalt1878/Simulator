using System.Collections.Generic;

namespace Windows.Browser.Sites.Email
{
	public class EmailPage : Site
	{
		public override string Name => "Email";
		public OpenedMail OpenedMail { get; private set; }
		// private List<Email> _inbox;

		void Awake()
		{
			OpenedMail = GetComponentInChildren<OpenedMail>();
			OpenedMail.gameObject.SetActive(false);
		}
	}
}