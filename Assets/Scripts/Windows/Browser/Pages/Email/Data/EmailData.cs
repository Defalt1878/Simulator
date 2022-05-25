using System;
using System.IO;
using UnityEngine;

namespace Windows.Browser.Pages.Email.Data
{
	[Serializable]
	public abstract class EmailData
	{
		public abstract string SenderName { get; }
		public abstract string Subject { get; }

		public abstract void OnLoad();
		public abstract void OnOpen();

		public Sprite AvatarSprite => _avatarSprite ??=
			Resources.Load<SpriteRenderer>(Path.Combine("Emails", EmailFolder, "Avatar")).sprite;

		public Transform Content => _content ??=
			Resources.Load<Transform>(Path.Combine("Emails", EmailFolder, "Content"));

		protected bool IsCompleted { get; set; }
		public bool IsRead { get; set; }

		private protected abstract string EmailFolder { get; }

		[NonSerialized] private Sprite _avatarSprite;
		[NonSerialized] private Transform _content;
		[NonSerialized] private protected Action<string, string> CheckComplete;
	}
}