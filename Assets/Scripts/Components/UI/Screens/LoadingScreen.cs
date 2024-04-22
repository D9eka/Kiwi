using Components.UI.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Components.UI
{
    public class LoadingScreen : ScreenComponent
    {
        [SerializeField] private GameObject _loadingContent;
        [SerializeField] private Slider _slider;

        public static LoadingScreen Instance;

        protected override void Start()
        {
            base.Start();

            if(Instance != null) 
            { 
                Destroy(Instance);
            }
            Instance = this;
        }

        public void Activate()
        {
            _slider.value = 0;
            _loadingContent.SetActive(true);
        }

        public void SetLoadingProgress(float loadingProgress)
        {
            _slider.value = loadingProgress;
        }
    }
}