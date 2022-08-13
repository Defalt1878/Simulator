using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UserData;

namespace Windows.Browser.Pages.Apps
{
	public class Downloader : MonoBehaviour
	{
		[SerializeField] private Button button;
		[SerializeField] private Image buttonImage;

		private const float DownloadTime = 4f;
		public bool Downloading { get; private set; }

		public bool Active
		{
			set => button.interactable = value;
		}

		private void Awake()
		{
			button ??= GetComponent<Button>();
			buttonImage ??= GetComponent<Image>();
		}

		public void StartDownload(App app)
		{
			Downloading = true;
			StartCoroutine(DownloadCoroutine(app));
		}

		private IEnumerator DownloadCoroutine(App app)
		{
			var defaultColors = button.colors;
			var modifiedColors = defaultColors;
			modifiedColors.disabledColor = Color.yellow;
			button.colors = modifiedColors;

			buttonImage.fillAmount = 0;
			for (var i = 1; i <= 100; i++)
			{
				yield return new WaitForSeconds(DownloadTime / 100);
				buttonImage.fillAmount += 0.01f;
			}

			StaticData.GetInstance().Apps.Download(app);
			button.colors = defaultColors;
		}
	}
}