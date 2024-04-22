using Components.Audio;
using Creatures.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Components.UI.Screens
{
    public class AudioSettingScreen : ScreenComponent
    {
        [SerializeField] private Slider _volumeSlider;

        private const string MUSIC_VOLUME_KEY = "MusicVolume";

        protected override void Start()
        {
            base.Start();
            _volumeSlider.value = PlayerPrefsController.GetFloat(MUSIC_VOLUME_KEY);
            _volumeSlider.onValueChanged.AddListener(value => Save(_volumeSlider.value));
        }

        private void Save(float value)
        {
            PlayerPrefsController.SetFloat(MUSIC_VOLUME_KEY, value);
            AudioHandler.Instance.LoadSettings();
        }
    }
}