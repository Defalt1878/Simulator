using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Browser
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Image loadingImage;
        private const float LoadingTime = 0.7f;
        private const int IterationsCount = 100;

        public void StartLoading()
        {
            gameObject.SetActive(true);
            loadingImage.fillAmount = 0;
            StartCoroutine(LoadingCoroutine());
        }

        private IEnumerator LoadingCoroutine()
        {
            for (var i = 0; i < IterationsCount; i++)
            {
                yield return new WaitForSeconds(LoadingTime / IterationsCount);
                loadingImage.fillAmount += 1f / IterationsCount;
            }

            OnLoadEnd?.Invoke();
            OnLoadEnd = null;
            gameObject.SetActive(false);
        }

        public event Action OnLoadEnd;
    }
}
