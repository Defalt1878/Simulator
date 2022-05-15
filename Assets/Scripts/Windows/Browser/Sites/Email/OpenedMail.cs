using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser.Sites.Email
{
	public class OpenedMail : MonoBehaviour
	{
		public EmailData EmailData
		{
			get => _emailData;
			set
			{
				_emailData = value;
				UpdateEmail();
			}
		}
		private EmailData _emailData;
		
		private Image _avatar;
		private Text _senderName;
		private Text _subject;
		private Transform _content;

		private void Awake()
		{
			var header = transform.Find("Header");
			_avatar = header.Find("Avatar").GetComponent<Image>();
			_senderName = header.Find("SenderName").GetComponent<Text>();
			_subject = header.Find("Subject").GetComponent<Text>();
			_content = transform.Find("Content");
		}

		private void UpdateEmail()
		{
			EmailData.OnOpen();
			_avatar.sprite = EmailData.AvatarSprite;
			_senderName.text = EmailData.SenderName;
			_subject.text = EmailData.Subject;
			Destroy(_content.gameObject);
			_content = Instantiate(EmailData.Content, transform);
		}
	}
}