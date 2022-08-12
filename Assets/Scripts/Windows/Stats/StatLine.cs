using System;
using TMPro;
using UnityEngine;
using UserData;

namespace Windows.Stats
{
	public class StatLine : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI statName;
		[SerializeField] private TextMeshProUGUI statValue;

		private Action<IStat> _onValueChangeAction;
		private IStat _stat;

		public IStat Stat
		{
			get => _stat;
			set
			{
				_stat = value ?? throw new ArgumentNullException();
				statName.text = _stat.Name;
				statValue.text = _stat.StrValue;
				_onValueChangeAction = stat => statValue.text = stat.StrValue;
				_stat.OnValueChanged += _onValueChangeAction;
			}
		}

		private void OnDestroy()
		{
			_stat.OnValueChanged -= _onValueChangeAction;
		}
	}
}