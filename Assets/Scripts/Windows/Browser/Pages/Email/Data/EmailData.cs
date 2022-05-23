using System;
using System.IO;
using UnityEngine;

namespace Windows.Browser.Pages.Email.Data
{
	public abstract class EmailData : ICloneable
	{
		public abstract string SenderName { get; }
		public abstract string Subject { get; }

		public abstract void OnLoad();
		public abstract void OnOpen();
		private protected abstract string EmailFolder { get; }

		public Sprite AvatarSprite => _avatarSprite ??=
			Resources.Load<SpriteRenderer>(Path.Combine("Emails", EmailFolder, "Avatar")).sprite;

		public Transform Content => _content ??=
			Resources.Load<Transform>(Path.Combine("Emails", EmailFolder, "Content"));

		private Sprite _avatarSprite;
		private Transform _content;
		private protected Action<string, string> CheckComplete;
		public object Clone()
		{
			throw new NotImplementedException();
		}
	}
}