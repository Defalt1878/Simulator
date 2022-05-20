using UnityEngine;
using UnityEngine.UI;

namespace Windows.Panel
{
	public class ExpandButton : MonoBehaviour
	{
		private Image _image;
		private bool _isExpanded;
		[SerializeField] private Sprite defaultSprite;
		[SerializeField] private Sprite expandedSprite;

		public void Awake()
		{
			_image = GetComponentInChildren<Image>();
			defaultSprite ??= _image.sprite;
			expandedSprite ??= defaultSprite;
		}

		private void OnClick()
		{
			_isExpanded = !_isExpanded;
			_image.sprite = _isExpanded
				? expandedSprite
				: defaultSprite;
			//TODO Реализовать функцию открытия на полный экран
		}
	}
}