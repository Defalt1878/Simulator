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
		[SerializeField] private OpenedMail openedMail;
		private List<string> _receivedEmails;

		public static readonly Dictionary<string, Func<string, EmailData>> EmailsData = new()
		{
			{StartEmail.Name, _ => new StartEmail()},
			{CmdEmail.Name, _ => new CmdEmail()},
			{MinerEmail.Name, _ => new MinerEmail()},
			{
				DarkMarketEmail.Name, s =>
				{
					if (s is null)
						return new DarkMarketEmail();
					var parts = s.Split(' ');
					return new DarkMarketEmail
					{
						HashRate = float.Parse(parts[1]),
						Server = parts[2]
					};
				}
			}
		};

		private void Awake()
		{
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

		private void InstantiateEmail(string emailData)
		{
			var parts = emailData.Split(' ');
			var email = Instantiate(emailPrefab, transform);
			email.EmailData = EmailsData[parts[0]].Invoke(emailData);
			email.OpenedMail = openedMail;
		}
	}
}