using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
	public class VolumeMixer : MonoBehaviour
	{
		[SerializeField] private Slider slider;
		[SerializeField] private MusicPlayer musicPlayer;
		[SerializeField] private TextMeshProUGUI volumeValue;

		public void OnValueChanged(float newValue)
		{
			musicPlayer.Volume = newValue;
			volumeValue.text = $"{musicPlayer.Volume * 100:0}%";
		}

		public void Awake()
		{
			slider.value = musicPlayer.Volume;
			volumeValue.text = $"{musicPlayer.Volume * 100:0}%";
		}
	}
}