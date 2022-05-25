using Windows.Browser.Pages.Email.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserData;

namespace Windows.Browser.Pages.Email
{
	public class Email : MonoBehaviour
	{
		[SerializeField] private GameObject newEmailNotification;
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
		private TextMeshProUGUI _senderName;
		private TextMeshProUGUI _subject;

		private void Awake()
		{
			_avatar = transform.Find("Avatar").GetComponent<Image>();
			_senderName = transform.Find("SenderName").GetComponent<TextMeshProUGUI>();
			_subject = transform.Find("Subject").GetComponent<TextMeshProUGUI>();
		}

		private void UpdateEmail()
		{
			_avatar.sprite = EmailData.AvatarSprite;
			_senderName.text = EmailData.SenderName;
			_subject.text = EmailData.Subject;
			// newEmailNotification.SetActive(StaticData.GetInstance().Emails.IsRead(EmailData.));
		}

		public void OnClick()
		{
			OpenedMail.gameObject.SetActive(true);
			OpenedMail.EmailData = EmailData;
		}
	}
}