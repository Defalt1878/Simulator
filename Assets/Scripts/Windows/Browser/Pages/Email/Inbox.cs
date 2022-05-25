using System.Collections.Generic;
using Windows.Browser.Pages.Email.Data;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.Email
{
	public class Inbox : MonoBehaviour
	{
		[SerializeField] private Email emailPrefab;
		[SerializeField] private OpenedMail openedMail;
		private List<EmailData> _receivedEmails;

		private void Awake()
		{
			_receivedEmails = StaticData.GetInstance().Emails;
		}

		private void Start()
		{
			foreach (var receivedEmail in _receivedEmails)
				InstantiateEmail(receivedEmail);
		}

		private void Update()
		{
			var childCount = transform.childCount;
			if (childCount == _receivedEmails.Count)
				return;
			for (var i = childCount; i < _receivedEmails.Count; i++)
				InstantiateEmail(_receivedEmails[i]);
		}

		private void InstantiateEmail(EmailData emailData)
		{
			var email = Instantiate(emailPrefab, transform);
			email.EmailData = emailData;
			email.OpenedMail = openedMail;
		}
	}
}