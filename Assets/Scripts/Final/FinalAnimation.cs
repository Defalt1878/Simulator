using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Final
{
	public class FinalAnimation : MonoBehaviour
	{
		[SerializeField] private Image background;
		[SerializeField] private TextMeshProUGUI content;
		private const float AnimationTime = 1.5f;
		private const int IterationsInCycle = 100;
		private bool _canPressSpace;
		private Action _onSpacePressed;

		public void StartAnimation(Action onSpacePressed = null)
		{
			_onSpacePressed = onSpacePressed;
			gameObject.SetActive(true);
			background.color -= new Color(0, 0, 0, 1);
			content.color -= new Color(0, 0, 0, 1);
			StartCoroutine(AnimationCoroutine());
		}

		private void Update()
		{
			if (_canPressSpace && Input.GetKey(KeyCode.Space))
				StartCoroutine(AnimationCoroutine(true));
		}

		private IEnumerator AnimationCoroutine(bool isEnd = false)
		{
			if (isEnd)
				_onSpacePressed?.Invoke();
			var transparencyChangingClr = new Color(0f, 0f, 0f, 1f / IterationsInCycle * (isEnd ? -1 : 1));
			for (var i = 0; i < IterationsInCycle; i++)
			{
				background.color += transparencyChangingClr;
				content.color += transparencyChangingClr;
				yield return new WaitForSeconds(AnimationTime / IterationsInCycle);
			}

			_canPressSpace = !_canPressSpace;
			if (isEnd)
				gameObject.SetActive(false);
		}
	}
}