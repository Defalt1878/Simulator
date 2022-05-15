using TMPro;
using UnityEngine;

namespace Windows.Stats
{
	public class StatLine : MonoBehaviour
	{
		public string Name
		{
			get => _statName.text;
			set => _statName.text = value;
		}
		
		public string Value
		{
			get => _statValue.text;
			set => _statValue.text = value;
		}
		
		private TextMeshProUGUI _statName;
		private TextMeshProUGUI _statValue;
	
		private void Awake()
		{
			_statName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
			_statValue = transform.Find("Value").GetComponent<TextMeshProUGUI>();
		}
	}
}
