using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UserData;

namespace Windows.Miner
{
	public class MiningProcess : MonoBehaviour
	{
		[SerializeField] private Image btcImage;
		private const float FullFillIterationSeconds = 8f;
		private const float OnceFillAmount = 0.005f;
		private const float WaitTime = FullFillIterationSeconds * OnceFillAmount / 2;
		private const float MiningUpdateTime = 5f;

		private void Awake()
		{
			StartCoroutine(ImageFillCoroutine());
			StartCoroutine(MiningCoroutine());
		}

		private IEnumerator ImageFillCoroutine()
		{
			var miningData = StaticData.GetInstance().MiningData;
			while (true)
			{
				if (miningData.UserHashRate > 0)
				{
					if (btcImage.fillClockwise)
						btcImage.fillAmount += OnceFillAmount;
					else
						btcImage.fillAmount -= OnceFillAmount;
					if (Math.Abs(btcImage.fillAmount) < 10e-5 || Math.Abs(btcImage.fillAmount - 1) < 10e-5)
						btcImage.fillClockwise = !btcImage.fillClockwise;
				}

				yield return new WaitForSeconds(WaitTime);
			}
			// ReSharper disable once IteratorNeverReturns
		}

		private static IEnumerator MiningCoroutine()
		{
			var miningData = StaticData.GetInstance().MiningData;
			var cryptoData = StaticData.GetInstance().CryptoData;
			while (true)
			{
				yield return new WaitForSeconds(MiningUpdateTime);
				var bitcoinsMined = miningData.UserHashRate / miningData.BtcHashRate * MiningUpdateTime;
				cryptoData.Bitcoin.Value += bitcoinsMined;
			}
			// ReSharper disable once IteratorNeverReturns
		}
	}
}