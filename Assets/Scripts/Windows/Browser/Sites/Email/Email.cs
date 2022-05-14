using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser.Sites.Email
{
	public class Email : MonoBehaviour
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

		private OpenedMail _openedMail;

		private Image _avatar;
		private Text _senderName;
		private Text _subject;

		private void Awake()
		{
			_avatar = transform.Find("Avatar").GetComponent<Image>();
			_senderName = transform.Find("SenderName").GetComponent<Text>();
			_subject = transform.Find("Subject").GetComponent<Text>();
			_openedMail = transform.GetComponentInParent<EmailPage>().OpenedMail;
		}

		private void UpdateEmail()
		{
			_avatar.sprite = EmailData.GetAvatarSprite();
			_senderName.text = EmailData.SenderName;
			_subject.text = EmailData.Subject;
		}

		public void OnClick()
		{
			_openedMail.gameObject.SetActive(true);
			_openedMail.EmailData = EmailData;
		}
	}
}