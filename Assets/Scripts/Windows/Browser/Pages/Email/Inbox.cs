using System;
using System.Collections.Generic;
using Windows.Browser.Pages.Email.Data;
using UnityEngine;
using UserData;

namespace Windows.Browser.Pages.Email
{
	public class Inbox : MonoBehaviour
	{
		[SerializeField] private Email emailPrefab;
		private OpenedMail _openedMail;
		private List<string> _receivedEmails;

		private static readonly Dictionary<string, EmailData> EmailsData = new()
		{
			{StartEmail.Name, new StartEmail()},
			{CmdEmail.Name, new CmdEmail()},
			{MinerEmail.Name, new MinerEmail()}
		};

		private void Awake()
		{
			_openedMail = transform.GetComponentInParent<EmailPage>().openedMail;
			_receivedEmails = StaticData.GetInstance().Emails.GetReceived();
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

		private void InstantiateEmail(string emailName)
		{
			var email = Instantiate(emailPrefab, transform);
			email.EmailData = EmailsData[emailName];
			email.OpenedMail = _openedMail;
		}
	}
}