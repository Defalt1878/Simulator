using System;

namespace UserData
{
	[Serializable]
	public class  UserStats
	{
		public Stat<float> Money { get; }

		public UserStats()
		{
			Money = new Stat<float>(nameof(Money), "${0:n}", value => value >= 0);
		}
	}
}