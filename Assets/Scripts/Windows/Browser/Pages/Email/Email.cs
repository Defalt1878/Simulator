using Windows.Browser.Pages.Email.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser.Pages.Email
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

		public OpenedMail OpenedMail { get; set; }

		private EmailData _emailData;
		private Image _avatar;
		private Text _senderName;
		private Text _subject;

		private void Awake()
		{
			_avatar = transform.Find("Avatar").GetComponent<Image>();
			_senderName = transform.Find("SenderName").GetComponent<Text>();
			_subject = transform.Find("Subject").GetComponent<Text>();
		}

		private void UpdateEmail()
		{
			_avatar.sprite = EmailData.AvatarSprite;
			_senderName.text = EmailData.SenderName;
			_subject.text = EmailData.Subject;
		}

		public void OnClick()
		{
			OpenedMail.gameObject.SetActive(true);
			OpenedMail.EmailData = EmailData;
		}
	}
}