using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Components.UI.Screens
{
    public class FadeScreen : ScreenComponent
    {
        public EventHandler OnEndFade;

        public static FadeScreen Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public override void Enter()
        {
            base.Enter();
            StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            float startValue = 0f;
            float endValue = 255f;
            float time_seconds = 3f;
            float initialTime = Time.time;
            float endTime = initialTime + time_seconds;
            while (Time.time <= endTime)
            {
                _content.GetComponent<Image>().color = new Color(0, 0, 0, Mathf.Lerp(startValue, endValue, Time.time / endTime));
                yield return new WaitForEndOfFrame();
            }
            OnEndFade?.Invoke(this, EventArgs.Empty);
        }
    }
}
