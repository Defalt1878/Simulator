using UnityEngine;

namespace Windows.Browser.Pages.Email
{
	public class EmailPage : Page
	{
		public override string Name => "Email";
		[SerializeField] public OpenedMail openedMail;
	}
}