using UnityEngine;

namespace Windows.Miner
{
	public class CellColor
	{
		public static CellColor DefaultColor => new(new Color(1f, 0.81f, 0.81f));
		public static CellColor CanBeSelected => new(new Color(1f, 0.87f, 0.23f));
		public static CellColor SelectedColor => new(new Color(0.65f, 1f, 0f));

		private readonly Color _color;

		private CellColor(Color color) =>
			_color = color;

		public static implicit operator Color(CellColor color) =>
			color._color;

		public static explicit operator CellColor(Color color) =>
			new(color);
	}
}