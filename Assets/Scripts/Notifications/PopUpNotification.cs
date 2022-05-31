using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Notifications
{
	public class PopUpNotification : MonoBehaviour
	{
		[SerializeField] private Image background;
		[SerializeField] private TextMeshProUGUI content;
		[SerializeField] private AudioSource audioSource;
		[SerializeField] private AudioClip defaultAudio;
		[SerializeField] private AudioClip warningAudio;
		[SerializeField] private AudioClip errorAudio;
		[SerializeField] private AudioClip successAudio;

		private const float NotificationAnimationTime = 4f;
		private const int IterationsInCycle = 200;
		private const float StartTransparency = 0.75f;
		private Coroutine _currentCoroutine;

		private string ContentText
		{
			set => content.text = value;
		}

		private NotificationType Type
		{
			set
			{
				background.color = value switch
				{
					NotificationType.Default => new Color(1f, 1f, 1f, StartTransparency),
					NotificationType.Warning => new Color(1f, 1f, 0f, StartTransparency),
					NotificationType.Error => new Color(1f, 0f, 0f, StartTransparency),
					NotificationType.Success => new Color(0f, 1f, 0f, StartTransparency),
					_ => throw new ArgumentOutOfRangeException()
				};
				content.color = new Color(0f, 0f, 0f, StartTransparency);

				audioSource.clip = value switch
				{
					NotificationType.Default => defaultAudio,
					NotificationType.Warning => warningAudio,
					NotificationType.Error => errorAudio,
					NotificationType.Success => successAudio,
					_ => throw new ArgumentOutOfRangeException()
				};
			}
		}

		public void Appear(string text, NotificationType type)
		{
			if (_currentCoroutine is not null)
				StopCoroutine(_currentCoroutine);
			ContentText = text;
			Type = type;
			gameObject.SetActive(true);
			_currentCoroutine = StartCoroutine(ShowCoroutine());
		}

		private IEnumerator ShowCoroutine()
		{
			audioSource.Play();
			var transparencyChangingClr = new Color(0f, 0f, 0f, StartTransparency / IterationsInCycle);
			yield return new WaitForSeconds(1.5f);
			for (var i = 0; i < IterationsInCycle; i++)
			{
				background.color -= transparencyChangingClr;
				content.color -= transparencyChangingClr;
				yield return new WaitForSeconds((NotificationAnimationTime - 1.5f) / IterationsInCycle);
			}

			gameObject.SetActive(false);
		}
	}
}