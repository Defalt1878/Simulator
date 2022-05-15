namespace Windows.Browser.Sites.Email
{
	public class EmailPage : Page
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