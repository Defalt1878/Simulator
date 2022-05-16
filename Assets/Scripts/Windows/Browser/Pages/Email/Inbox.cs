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

		private static readonly Dictionary<string, EmailData> EmailsData = new()
		{
			{"First", new FirstEmail()},
			{"Miner", new MinerEmail()}
		};

		private void Awake()
		{
			_openedMail = transform.GetComponentInParent<EmailPage>().openedMail;
		}

		private void Start()
		{
			foreach (var receivedEmail in StaticData.GetInstance().ReceivedEmails)
			{
				var email = Instantiate(emailPrefab, transform);
				email.EmailData = EmailsData[receivedEmail];
				email.OpenedMail = _openedMail;
			}
		}
	}
}