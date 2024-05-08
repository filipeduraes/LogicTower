using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LogicTower.LevelManagement
{
    public class LoadTransitionHandler : MonoBehaviour
    {
        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeDuration = 1f;

        public event Action OnTransitionFinished = delegate { };

        public void FadeOut()
        {
            StartCoroutine(StartFadeOut());
        }

        public void FadeIn()
        {
            StartCoroutine(Fade(1f, 0f));
        }

        private IEnumerator StartFadeOut()
        {
            yield return Fade(0f, 1f);
            OnTransitionFinished();
        }
        
        private IEnumerator Fade(float initialAlpha, float finalAlpha)
        {
            float timer = 0f;

            while (timer <= fadeDuration)
            {
                Color fadeColor = fadeImage.color;
                fadeColor.a = Mathf.Lerp(initialAlpha, finalAlpha, timer / fadeDuration);
                fadeImage.color = fadeColor;

                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}