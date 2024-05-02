using Components.UI.Screens;
using Creatures.Player;
using Extensions;
using Sections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Components.UI
{
    public class MainMenuScreen : ScreenComponent
    {
        [SerializeField] private AudioClip _backgroundMusic;
        [Space]
        [SerializeField] private GameObject _contunueButton;
        [Space]
        [SerializeField] private SectionSO _continueGameSection;

        protected override void Start()
        {
            base.Start();

            _contunueButton.GetComponent<Button>().interactable = PlayerPrefsController.IsCompleteTutorial();
        }

        public void StartNewGame()
        {
            PlayerPrefsController.CleanPlayerInfo();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ContinueGame()
        {
            PlayerPrefsController.CleanRunInfo();
            new SceneManager().LoadScene(_continueGameSection.SectionName, _continueGameSection.StartPosition);
        }
    }
}