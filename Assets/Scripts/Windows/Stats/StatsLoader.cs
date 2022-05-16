using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UserData;

namespace Windows.Stats
{
	public class StatsLoader : MonoBehaviour
	{
		[SerializeField] private StatLine statLine;
		private Dictionary<string, StatLine> _lines;
		private GameStats _stats;
		private Action<string, string> _onChangeAction;

		private void Awake()
		{
			_lines = new Dictionary<string, StatLine>();
			_stats = StaticData.GetInstance().Stats;
			foreach (var property in _stats.GetType().GetProperties().Where(prop => prop.Name[^3..] == "Str"))
			{
				var instLine = Instantiate(statLine, transform);
				var statName = property.Name[..^3];
				instLine.Name = statName;
				instLine.Value = property.GetValue(_stats).ToString();
				_lines[statName] = instLine;
			}

			_onChangeAction = (statName, newValue) => _lines[statName].Value = newValue;
			_stats.OnValueChanged += _onChangeAction;
		}

		private void OnDestroy()
		{
			_stats.OnValueChanged -= _onChangeAction;
		}
	}
}