using UnityEngine;

namespace Windows.Browser.Sites.Email
{
    public interface IEmailData
    {
        public Sprite AvatarSprite { get; }
        public string SenderName { get; }
        public string Subject { get; }
        public string Text { get; }
    }
}
