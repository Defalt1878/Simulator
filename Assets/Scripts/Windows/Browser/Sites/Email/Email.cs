using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser.Sites.Email
{
	public class Email : MonoBehaviour
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
			if (EmailData.AvatarSprite != null)
				_avatar.sprite = EmailData.AvatarSprite;
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