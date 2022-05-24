using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Windows.Miner
{
	public class GameCell : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
	{
		[SerializeField] private Image cellImage;
		[SerializeField] private Sprite defaultSprite;
		[SerializeField] private Sprite startSprite;
		[SerializeField] private Sprite exitSprite;
		[SerializeField] private Sprite targetSprite;
		private Dictionary<CellType, Sprite> _cellTypeSprites;

		private void Awake()
		{
			_cellTypeSprites = new Dictionary<CellType, Sprite>
			{
				{CellType.Default, defaultSprite},
				{CellType.Start, startSprite},
				{CellType.Exit, exitSprite},
				{CellType.Target, targetSprite}
			};
			Type = CellType.Default;
		}

		private CellType _type;

		public CellType Type
		{
			get => _type;
			set
			{
				_type = value;
				cellImage.sprite = _cellTypeSprites[_type];
			}
		}

		public GameField CurrentField { get; set; }
		public Vector2Int Position { get; set; }

		public Color Color
		{
			get => cellImage.color;
			set => cellImage.color = value;
		}

		private bool _isTarget;

		public void OnPointerDown(PointerEventData eventData) => CurrentField.CellMouseDown(this);

		public void OnPointerEnter(PointerEventData eventData) => CurrentField.CellMouseEnter(this);
	}
}