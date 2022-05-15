using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UserData;

namespace Windows.Browser.Pages.Apps
{
	public class DownloadButton : MonoBehaviour
	{
		private const float DownloadTime = 4f;
		public string DownloadingAppName { get; set; }
		private Image _buttonImage;
		private Button _button;
		public bool Downloading { get; private set; }
		public bool Active
		{
			get => _button.interactable;
			set => _button.interactable = value;
		}

		private void Awake()
		{
			_buttonImage = GetComponent<Image>();
			_button = GetComponent<Button>();
			Downloading = false;
		}

		public void OnClick()
		{
			Downloading = true;
			StartCoroutine(DownloadCoroutine());
		}

		private IEnumerator DownloadCoroutine()
		{
			var defaultColors = _button.colors;
			var modifiedColors = defaultColors;
			modifiedColors.disabledColor = Color.yellow;
			_button.colors = modifiedColors;

			_buttonImage.fillAmount = 0;
			for (var i = 1; i <= 100; i++)
			{
				yield return new WaitForSeconds(DownloadTime / 100);
				_buttonImage.fillAmount = i / 100f;
			}

			StaticData.GetInstance().Shortcuts.Add(DownloadingAppName);
			StaticData.GetInstance().AvailableToDownloadApps.Remove(DownloadingAppName);
			_button.colors = defaultColors;
		}
	}
}