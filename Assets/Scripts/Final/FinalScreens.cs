using UnityEngine;

namespace Final
{
	public class FinalScreens : MonoBehaviour
	{
		[SerializeField] private FinalAnimation finalPayScreen;
		[SerializeField] private FinalAnimation unknownFinalScreen;
		[SerializeField] private FinalAnimation gameFailedScreen;

		public static FinalAnimation FinalPayScreen { get; private set; }
		public static FinalAnimation UnknownFinalScreen { get; private set; }
		public static FinalAnimation GameFailedScreen { get; private set; }

		private void Awake()
		{
			FinalPayScreen = finalPayScreen;
			UnknownFinalScreen = unknownFinalScreen;
			GameFailedScreen = gameFailedScreen;
		}
	}
}