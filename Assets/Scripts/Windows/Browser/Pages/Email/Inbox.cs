using System.Collections.Generic;
using Windows.Browser.Pages.Email.Data;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.Email
{
	public class Inbox : MonoBehaviour
	{
		[SerializeField]private Email emailPrefab;
		private OpenedMail _openedMail;

		private static readonly List<EmailData> EmailsData = new()
		{
			new FirstEmail()
		};

		private void Awake()
		{
			_openedMail = transform.GetComponentInParent<EmailPage>().openedMail;
		}

		private void Start()
		{
			for (var i = 0; i < StaticData.GetInstance().ReceivedEmailsCount; i++)
			{
				var email = Instantiate(emailPrefab, transform);
				email.EmailData = EmailsData[i];
				email.OpenedMail = _openedMail;
			}
		}
	}
}