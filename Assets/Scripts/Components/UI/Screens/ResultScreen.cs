using Creatures.Player;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Components.UI.Screens
{
    public class ResultScreen : ScreenComponent
    {
        [SerializeField] private TextMeshProUGUI _header;
        [SerializeField] private TextMeshProUGUI _endDemoText;
        [SerializeField] private GameObject _lastEnemyHandler;
        [SerializeField] private Image _lastEnemyImage;
        [Space]
        [SerializeField] private TextMeshProUGUI _playtime;
        [SerializeField] private TextMeshProUGUI _enemyDefeated;
        [SerializeField] private TextMeshProUGUI _roomPassed;
        [SerializeField] private TextMeshProUGUI _essenceEarned;
        [SerializeField] private TextMeshProUGUI _updatesCreated;
        [SerializeField] private TextMeshProUGUI _amountDamage;
        [SerializeField] private TextMeshProUGUI _maxDamage;
        [SerializeField] private TextMeshProUGUI _damageEarned;
        [SerializeField] private TextMeshProUGUI _damageHealed;

        public static ResultScreen Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public override void Enter()
        {
            base.Enter();
            Fill();
        }

        public void Fill()
        {
            if (MyGameManager.LastDamageEnemy != null)
                _lastEnemyImage.sprite = MyGameManager.LastDamageEnemy;

            _playtime.text = TimeSpan.FromSeconds(Time.time - MyGameManager.StartTime).ToString("mm':'ss");
            _enemyDefeated.text = MyGameManager.EnemyDefeated.ToString();
            _roomPassed.text = MyGameManager.RoomPassed.ToString();
            _essenceEarned.text = MyGameManager.EssenceEarned.ToString();
            _updatesCreated.text = MyGameManager.UpdatesCreated.ToString();
            _amountDamage.text = MyGameManager.AmountDamage.ToString();
            _maxDamage.text = MyGameManager.MaxDamage.ToString();
            _damageEarned.text = MyGameManager.DamageEarned.ToString();
            _damageHealed.text = MyGameManager.DamageHealed.ToString();
        }

        public void SetEndDemo()
        {
            _header.text = "";
            _lastEnemyHandler.SetActive(false);
            _endDemoText.gameObject.SetActive(true);
        }

        public void Return()
        {
            PlayerPrefsController.CleanRunInfo();
            UIController.Instance.PushScreen(LoadingScreen.Instance);
            if (SoundManager.Instance != null)
                Destroy(SoundManager.Instance.gameObject);
            SceneManager.LoadScene(0);
        }
    }
}
