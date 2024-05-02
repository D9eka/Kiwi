using Components.UI;
using Components.UI.Screens;
using Creatures.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Screen = UnityEngine.Screen;

namespace Assets.Scripts.Components.UI.Screens
{
    public class SettingsScreen : ScreenComponent
    {
        [SerializeField] private ValueSlider _resolutionSlider;
        [SerializeField] private Toggle _fullscreenToggle;
        [Space]
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        protected override void Start()
        {
            base.Start();

            List<string> screenResolutions = Screen.resolutions
                                             .Where(screen => screen.refreshRateRatio.numerator == Screen.currentResolution.refreshRateRatio.numerator)
                                             .Select(screen => $"{screen.width}x{screen.height}:{screen.refreshRateRatio}")
                                             .ToList();
            int index = screenResolutions
                        .IndexOf($"{Screen.currentResolution.width}x{Screen.currentResolution.height}:{Screen.currentResolution.refreshRateRatio}");
            _resolutionSlider.SetValues(screenResolutions);
            _resolutionSlider.SetValueIndex(index);
            _resolutionSlider.OnChangeValue += ResolutionSlider_OnChangeValue;

            _fullscreenToggle.isOn = Screen.fullScreen;

            _soundSlider.value = PlayerPrefsController.GetFloat(SoundManager.SOUND_VOLUME_KEY, 1f);
            _musicSlider.value = PlayerPrefsController.GetFloat(SoundManager.MUSIC_VOLUME_KEY, 1f);
        }

        private void ResolutionSlider_OnChangeValue(object sender, string screenResolution)
        {
            Resolution resolution = Screen.resolutions.First(screen => $"{screen.width}x{screen.height}:{screen.refreshRateRatio}" == screenResolution);
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio);
        }

        public void ChangeFullScreenState(bool state)
        {
            Screen.fullScreen = state;
        }

        public void ChangeSoundVolume(float volume)
        {
            PlayerPrefsController.SetFloat(SoundManager.SOUND_VOLUME_KEY, volume);
            SoundManager.Instance.ChangeVolume();
        }

        public void ChangeMusicVolume(float volume)
        {
            PlayerPrefsController.SetFloat(SoundManager.MUSIC_VOLUME_KEY, volume);
        }
    }
}
