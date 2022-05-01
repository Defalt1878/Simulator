using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser.Sites.Email
{
	public class OpenedMail : MonoBehaviour
	{
		public IEmailData EmailData
		{
			get => _emailData;
			set
			{
				_emailData = value;
				UpdateEmail();
			}
		}
		private IEmailData _emailData;
		
		private Image _avatar;
		private Text _senderName;
		private Text _subject;
		private Text _text;

		void Awake()
		{
			_avatar = transform.Find("Avatar").GetComponent<Image>();
			_senderName = transform.Find("SenderName").GetComponent<Text>();
			_subject = transform.Find("Subject").GetComponent<Text>();
			_text = transform.Find("Text").GetComponent<Text>();
		}

		private void UpdateEmail()
		{
			_avatar.sprite = EmailData.AvatarSprite;
			_senderName.text = EmailData.SenderName;
			_subject.text = EmailData.Subject;
			_text.text = EmailData.Text;
		}
	}
}