using System;

namespace UserData
{
	[Serializable]
	public class Stat<TValue> : IStat
	{
		public string Name { get; }
		public string StrValue => string.Format(_strValueFormat, Value);
		protected Func<TValue, bool> ValueChecker { get; }

		public TValue Value
		{
			get => _value;
			set
			{
				if (!ValueChecker(value))
					throw new ArgumentException();

				_value = value;
				OnValueChanged?.Invoke(this);
			}
		}

		private TValue _value;
		private readonly string _strValueFormat;

		public Stat(string name, string strValueFormat, Func<TValue, bool> valueChecker)
		{
			Name = name;
			_strValueFormat = strValueFormat;
			ValueChecker = valueChecker;
		}

		[field: NonSerialized] public event Action<IStat> OnValueChanged;
	}
}