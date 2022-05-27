using System;

namespace UserData
{
	[Serializable]
	public class GameStats
	{
		private float _money;

		public float Money
		{
			get => _money;
			set
			{
				if (value < 0)
					throw new ArgumentException();
				_money = value;
				OnValueChanged?.Invoke(nameof(Money), MoneyStr);
			}
		}

		public string MoneyStr => $"${Money:n}";

		[field: NonSerialized] public event Action<string, string> OnValueChanged;
	}
}