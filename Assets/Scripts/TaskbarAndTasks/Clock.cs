using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UserData;

namespace TaskbarAndTasks
{
	public class Clock : MonoBehaviour
	{
		private const float TimeFactorRealToGame = 288;
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
			var instance = StaticData.GetInstance();
			while (instance.CurrentTime - instance.StartTime < TimeSpan.FromDays(1))
			{
				instance = StaticData.GetInstance();
				instance.CurrentTime = instance.CurrentTime.AddMinutes(1);
				yield return new WaitForSeconds(WaitTime);
				UpdateTimeOutput(instance.CurrentTime);
			}
			OnStop?.Invoke();
		}

		public event Action OnStop;

		private void UpdateTimeOutput(DateTime time) => _output.text = time.ToString(OutputFormat);
	}
}