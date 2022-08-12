using System;

namespace UserData
{
	public interface IStat
	{
		string Name { get; }
		string StrValue { get; }

		public event Action<IStat> OnValueChanged;
	}
}