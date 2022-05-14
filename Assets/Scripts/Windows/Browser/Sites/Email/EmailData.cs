using System;
using System.IO;
using UnityEngine;

namespace Windows.Browser.Sites.Email
{
	public abstract class EmailData
	{
		public abstract string SenderName { get; }
		public abstract string Subject { get; }
		private protected abstract string EmailFolder { get; }

		public Sprite GetAvatarSprite() =>
			Resources.Load<SpriteRenderer>(Path.Combine("Emails", EmailFolder, "Avatar")).sprite;
		
		public Transform GetContent() => Resources.Load<Transform>(Path.Combine("Emails", EmailFolder, "Content"));
	}
}