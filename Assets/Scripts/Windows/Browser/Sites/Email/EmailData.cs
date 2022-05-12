using System.IO;
using UnityEngine;

namespace Windows.Browser.Sites.Email
{
	public abstract class EmailData
	{
		public Sprite AvatarSprite =>
			Resources.Load<SpriteRenderer>(Path.Combine("Emails", EmailFolder, "Avatar")).sprite;

		public abstract string SenderName { get; }
		public abstract string Subject { get; }
		public Transform Content => Resources.Load<Transform>(Path.Combine("Emails", EmailFolder, "Content"));

		private protected abstract string EmailFolder { get; }
	}
}