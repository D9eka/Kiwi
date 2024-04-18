using Components.Audio;
using UnityEngine;
using Creatures.Player;

namespace Components.UI
{
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private AudioClip _backgroundMusic;
        [Space]
        [SerializeField] private GameObject _contunueButton;

        private void Start()
        {
            if (_backgroundMusic != null)
                AudioHandler.Instance.PlayMusic(_backgroundMusic);

            //_contunueButton.SetActive(PlayerPrefsController.HaveData());
        }
    }
}