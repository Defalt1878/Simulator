using System;
using System.Collections.Generic;
using UnityEngine;
using UserData;

namespace Windows.Stats
{
	public class StatsLoader : MonoBehaviour
	{
		[SerializeField] private StatLine statLine;
		private Dictionary<string, StatLine> _lines;

		private void Awake()
		{
			_lines = new Dictionary<string, StatLine>();
			var stats = StaticData.GetInstance().Stats;
			foreach (var property in stats.GetType().GetProperties())
			{
				var instLine = Instantiate(statLine, transform);
				instLine.Name = property.Name;
				instLine.Value = property.GetValue(stats).ToString();
				_lines[instLine.Name] = instLine;
			}

			stats.OnValueChanged += (statName, newValue) => _lines[statName].Value = newValue;
		}
	}
}