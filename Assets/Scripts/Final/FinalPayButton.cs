using System.Linq;
using Windows.Browser.Pages.Email.Data;
using UnityEngine;
using UnityEngine.UI;
using UserData;

namespace Final
{
	public class FinalPayButton : MonoBehaviour
	{
		private FinalAnimation _finalAnimation;

		private void Awake()
		{
			_finalAnimation = FinalScreens.FinalPayScreen;
		}

		public void OnClick()
		{
			var instance = StaticData.GetInstance();
			if (instance.Stats.Money < 3000)
				return;
			GetComponent<Button>().interactable = false;
			instance.Stats.Money -= 3000;
			var emails = instance.Emails;
			(emails.Single(data => data is StartEmail) as StartEmail)?.GameFinished();
			if (emails.SingleOrDefault(data => data is UnknownFinalEmail) is UnknownFinalEmail email)
				emails.Remove(email);
			_finalAnimation.StartAnimation();
		}
	}
}