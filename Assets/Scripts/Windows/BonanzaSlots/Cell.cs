using UnityEngine;
using UnityEngine.UI;

namespace Windows.BonanzaSlots
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] private Image image;
		// [SerializeField] private Sprite bananaSprite;
		// [SerializeField] private Sprite grapeSprite;
		// [SerializeField] private Sprite appleSprite;
		// [SerializeField] private Sprite watermelonSprite;
		// [SerializeField] private Sprite cherrySprite;
		// [SerializeField] private Sprite candySprite;
		// [SerializeField] private Sprite bomb2Sprite;
		// [SerializeField] private Sprite bomb5Sprite;
		// [SerializeField] private Sprite bomb10Sprite;
		// [SerializeField] private Sprite bomb50Sprite;
		// [SerializeField] private Sprite bomb100Sprite;
		
		public CellState State { get; set; }

		public CellState UpdateState()
		{
			return State = (CellState) Random.Range(0, 11);
		}
	}
}