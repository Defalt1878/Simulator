using System.IO;
using UnityEngine;

namespace Windows.Browser.Sites.Email
{
	public class FirstEmail : EmailData
	{
		public override string SenderName => "Неизвестный";
		public override string Subject => "Начало";
		private protected override string EmailFolder => "First";
	}
}