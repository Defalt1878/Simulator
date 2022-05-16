using System;
using System.IO;
using UnityEngine;

namespace Windows.Browser.Pages.Email.Data
{
	public abstract class EmailData
	{
		public abstract string SenderName { get; }
		public abstract string Subject { get; }
		
		private protected abstract Action OnComplete { get; }

		public Sprite AvatarSprite => _avatarSprite ??=
			Resources.Load<SpriteRenderer>(Path.Combine("Emails", EmailFolder, "Avatar")).sprite;

		public Transform Content => _content ??=
			Resources.Load<Transform>(Path.Combine("Emails", EmailFolder, "Content"));

		public abstract Action OnOpen { get; }

		private protected abstract string EmailFolder { get; }
		private Sprite _avatarSprite;
		private Transform _content;
	}
}