using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Windows.Browser.Sites.Email
{
	public class Inbox : MonoBehaviour
	{
		private Email _emailPrefab;

		private static readonly List<EmailData> EmailsData = new()
		{
			new FirstEmail()
		};

		// public static void AddEmail(EmailData emailData) => StaticData.GetInstance().ReceivedEmailsCount.Add(emailData);

		private void Awake()
		{
			_emailPrefab = Resources.Load<Email>(Path.Combine("Windows", "Browser", "Email", "Mail"));
		}

		private void Start()
		{
			for (var i = 0; i < StaticData.GetInstance().ReceivedEmailsCount; i++)
			{
				var email = Instantiate(_emailPrefab, transform);
				email.EmailData = EmailsData[i];
			}
		}
	}
}