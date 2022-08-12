using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UserData;

namespace TaskbarAndTasks
{
	public class Clock : MonoBehaviour
	{
		private const float TimeFactorRealToGame = 200;
		private const string OutputFormat = "h:mm tt\ndd/MM/yy";
		private const float WaitTime = 60 / TimeFactorRealToGame;

		private TextMeshProUGUI _output;

		private void Awake()
		{
			_output = GetComponent<TextMeshProUGUI>();
			StartCoroutine(TimeFlowCoroutine());
		}

		private IEnumerator TimeFlowCoroutine()
		{
			while (true)
			{
				var instance = StaticData.GetInstance();
				instance.CurrentInGameTime = instance.CurrentInGameTime.AddMinutes(1);
				yield return new WaitForSeconds(WaitTime);
				UpdateTimeOutput(instance.CurrentInGameTime);
				if (instance.CurrentInGameTime.Minute == 0)
					OnHourLast?.Invoke(instance.CurrentInGameTime);
			}
			// ReSharper disable once IteratorNeverReturns
		}

		public event Action<DateTime> OnHourLast;

		private void UpdateTimeOutput(DateTime time) => _output.text = time.ToString(OutputFormat);
	}
}