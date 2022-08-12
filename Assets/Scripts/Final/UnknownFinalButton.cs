using System.Linq;
using Windows.Browser.Pages.Email.Data;
using UnityEngine;
using UnityEngine.UI;
using UserData;

namespace Final
{
	public class UnknownFinalButton : MonoBehaviour
	{
		private FinalAnimation _finalAnimation;

		private void Awake()
		{
			_finalAnimation = FinalScreens.UnknownFinalScreen;
		}

		public void OnClick()
		{
			var instance = StaticData.GetInstance();
			if (instance.Stats.Money.Value < 1500)
				return;
			GetComponent<Button>().interactable = false;
			instance.Stats.Money.Value -= 1500;
			var emails = instance.Emails;
			(emails.Single(data => data is StartEmail) as StartEmail)?.GameFinished();
			if (emails.SingleOrDefault(data => data is FinalPayEmail) is FinalPayEmail email)
				emails.Remove(email);
			_finalAnimation.StartAnimation(StaticData.DataSaver.ResetData);
		}
	}
}