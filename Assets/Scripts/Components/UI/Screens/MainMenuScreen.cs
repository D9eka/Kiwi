using Components.Audio;
using Components.UI.Screens;
using DataService;
using Extensions;
using Sections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.UI
{
    public class MainMenuScreen : ScreenComponent
    {
        [SerializeField] private AudioClip _backgroundMusic;
        [Space]
        [SerializeField] private GameObject _contunueButton;
        [Space]
        [SerializeField] private SectionSO _newGameSection;
        [SerializeField] private SectionSO _continueGameSection;

        protected override void Start()
        {
            base.Start();

            if (_backgroundMusic != null)
                AudioHandler.Instance.PlayMusic(_backgroundMusic);

            //_contunueButton.SetActive(PlayerPrefsController.HaveData());
        }

        public void StartNewGame()
        {
            JsonDataService.DeleteAllData(true);
            new SceneManager().LoadScene(_newGameSection.SectionName, _newGameSection.StartPosition);
        }

        public void ContinueGame()
        {
            JsonDataService.DeleteAllData();
            new SceneManager().LoadScene(_continueGameSection.SectionName, _continueGameSection.StartPosition);
        }
    }
}