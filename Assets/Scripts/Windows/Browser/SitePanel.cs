using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser
{
	public class SitePanel : MonoBehaviour
	{
		public GameObject CurrentPage { get; set; }

		public string Name
		{
			get => _text.text;
			set => _text.text = value;
		}

		// private BackButton _backButton;
		private Text _text;

		private void Awake()
		{
			_text = GetComponentInChildren<Text>();
		}

		public void CloseTab()
		{
			Destroy(CurrentPage);
			gameObject.SetActive(false);
		}
	}
}