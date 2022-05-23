using Windows.Browser.Pages.Email.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser.Pages.Email
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
		private TextMeshProUGUI _senderName;
		private TextMeshProUGUI _subject;
		private Transform _content;

		private void Awake()
		{
			var header = transform.Find("Header");
			_avatar = header.Find("Avatar").GetComponent<Image>();
			_senderName = header.Find("SenderName").GetComponent<TextMeshProUGUI>();
			_subject = header.Find("Subject").GetComponent<TextMeshProUGUI>();
			_content = transform.Find("Content");
		}

		private void UpdateEmail()
		{
			EmailData.OnOpen();
			_avatar.sprite = EmailData.AvatarSprite;
			_senderName.text = EmailData.SenderName;
			_subject.text = EmailData.Subject;
			if (_content is not null)
				Destroy(_content.gameObject);
			_content = Instantiate(EmailData.Content, transform);
			if (EmailData is DarkMarketEmail darkData)
				_content.GetComponentInChildren<TMP_InputField>().text = darkData.Server;
		}
	}
}