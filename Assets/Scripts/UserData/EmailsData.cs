using System;
using System.Collections.Generic;

namespace UserData
{
	[Serializable]
	public class EmailsData
	{
		private readonly List<string> _receivedEmails;
		private readonly HashSet<string> _completedEmails;
		private readonly HashSet<string> _openedEmails;

		public EmailsData(params string[] receivedEmails)
		{
			_receivedEmails = new List<string>(receivedEmails);
			_completedEmails = new HashSet<string>();
			_openedEmails = new HashSet<string>();
		}

		public List<string> GetReceived() => _receivedEmails;
		public void NewEmail(string email) => _receivedEmails.Add(email);
		
		public void Complete(string email) => _completedEmails.Add(email);
		public void MarkOpen(string email) => _openedEmails.Add(email);
		public bool IsCompleted(string email) => _completedEmails.Contains(email);
		public bool IsOpened(string email) => _openedEmails.Contains(email);
	}
}