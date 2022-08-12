using System.Linq;
using UnityEngine;
using UserData;

namespace Windows.Stats
{
	public class StatsLoader : MonoBehaviour
	{
		[SerializeField] private StatLine statLine;

		private void Start()
		{
			var stats = StaticData.GetInstance().Stats;

			foreach (var stat in stats.GetType().GetProperties()
				         .Select(property => property.GetValue(stats) as IStat)
				         .Where(stat => stat is not null)
			        )
			{
				var instLine = Instantiate(statLine, transform);
				instLine.Stat = stat;
			}
		}
	}
}