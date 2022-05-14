using System.Collections;
using DesktopShortcuts;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser.Sites.Apps
{
	public class DownloadButton : MonoBehaviour
	{
		private const float DownloadTime = 4f;
		public string DownloadingAppName { get; set; }
		private Image _buttonImage;
		private Button _button;

		private void Awake()
		{
			_buttonImage = GetComponent<Image>();
			_button = GetComponent<Button>();
		}

		public void OnClick()
		{
			StaticData.GetInstance().AvailableToDownloadApps.Remove(DownloadingAppName);
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
			_button.colors = defaultColors;
		}
	}
}