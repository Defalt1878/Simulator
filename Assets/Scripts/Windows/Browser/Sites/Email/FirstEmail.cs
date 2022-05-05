using UnityEngine;

namespace Windows.Browser.Sites.Email
{
	public class FirstEmail : IEmailData
	{
		public Sprite AvatarSprite => null; //Resources.Load<Sprite>();
		public string SenderName => "Sender1";
		public string Subject => "Subject1";
		public string Text => "Text1";
	}
}