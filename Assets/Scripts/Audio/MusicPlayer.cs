using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
	public class MusicPlayer : MonoBehaviour
	{
		[SerializeField] private AudioSource audioSource;
		[SerializeField] private List<AudioClip> audioClips;

		private void Update()
		{
			if (audioSource.isPlaying)
				return;
			var currentClip = audioSource.clip;
			while (audioSource.clip == currentClip)
				audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
			audioSource.Play();
		}

		public float Volume
		{
			get => audioSource.volume;
			set => audioSource.volume = value;
		}
	}
}