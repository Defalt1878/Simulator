using UnityEngine;
using UnityEngine.UI;

namespace Windows.Miner
{
	public class GameCell : MonoBehaviour
	{
		[SerializeField] private Image image;
		[SerializeField] private Sprite defaultSprite;
		[SerializeField] private Sprite targetSprite;
		public GameField CurrentField { get; set; }
		public Vector2Int Position { get; set; }

		public bool IsTarget
		{
			get => _isTarget;
			set
			{
				_isTarget = value;
				image.sprite = _isTarget
					? targetSprite
					: defaultSprite;
			}
		}

		public Color Color
		{
			get => image.color;
			set => image.color = value;
		}

		private bool _isTarget;

		public void OnPointerDown() => CurrentField.CellMouseDown(this);

		public void OnPointerEnter() => CurrentField.CellMouseEnter(this);
	}
}