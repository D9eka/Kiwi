using Components.Audio;
using UnityEngine;
using Creatures.Player;
using Components.UI.Screens;

namespace Components.UI
{
    public class MainMenuScreen : ScreenComponent
    {
        [SerializeField] private AudioClip _backgroundMusic;
        [Space]
        [SerializeField] private GameObject _contunueButton;

        protected override void Start()
        {
            base.Start();

            if (_backgroundMusic != null)
                AudioHandler.Instance.PlayMusic(_backgroundMusic);

            //_contunueButton.SetActive(PlayerPrefsController.HaveData());
        }
    }
}