using UnityEngine;

namespace Windows.Cmd
{
	public class CmdColor
	{
		public static CmdColor Default => new(Color.white);
		public static CmdColor UserInput => new(Color.green);
		public static CmdColor Important => new(Color.yellow);
		public static CmdColor Error => new(Color.red);

		private readonly Color _color;

		private CmdColor(Color color) =>
			_color = color;

		public static implicit operator Color(CmdColor color) =>
			color._color;
	}
}