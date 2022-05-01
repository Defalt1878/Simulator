using UnityEngine;
using UnityEngine.UI;

namespace Windows.Panel
{
	public class ExpandButton : MonoBehaviour
	{
		private Image _image;
		private Transform _window;
		private bool _isExpanded;
		[SerializeField] private Sprite defaultSprite;
		[SerializeField] private Sprite expandedSprite;

		public void Awake()
		{
			_image = GetComponentInChildren<Image>();
			_window = GetComponentInParent<Window>().transform;
		
			defaultSprite ??= _image.sprite;
			expandedSprite ??= defaultSprite;
		}

		public void OnClick()
		{
			_isExpanded = !_isExpanded;
			_image.sprite = _isExpanded
				? expandedSprite
				: defaultSprite;
		}

		private void Expand()
		{
			// _window.
		}
	}
}