using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Windows.Browser.Sites.Email
{
	public class Inbox : MonoBehaviour
	{
		private Email _emailPrefab;
		private static readonly List<IEmailData> ReceivedEmails = new List<IEmailData>
		{
			new FirstEmail()
		};

		public void AddEmail(IEmailData emailData) => ReceivedEmails.Add(emailData);

		private void Awake()
		{
			_emailPrefab = Resources.Load<Email>(Path.Combine("Windows", "Browser", "Email", "Mail"));
		}

		private void Start()
		{
			foreach (var receivedEmail in ReceivedEmails)
			{
				var email = Instantiate(_emailPrefab, transform);
				email.EmailData = receivedEmail;
			}
		}
	}
}