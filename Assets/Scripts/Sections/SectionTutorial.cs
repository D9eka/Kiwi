using Components.ColliderBased;
using Components.UI;
using Components.UI.Screens;
using Creatures.AI;
using Creatures.Enemy;
using Creatures.Player;
using Environment.Doors;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sections
{
    public class SectionTutorial : MonoBehaviour
    {
        [SerializeField] private bool _startWaveOnAwake;
        [SerializeField] private bool _isOxygenWasting;
        [SerializeField] private bool _giveChip;
        [SerializeField] private bool _isFinalSection;
        [Header("Music")]
        [SerializeField] private AudioClip _backgroundMusic;
        [SerializeField] private AudioClip _startWaveSound;
        [Header("Doors")]
        [SerializeField] private TutorialDoor _startDoor;
        [SerializeField] private TutorialDoor _endDoor;
        [Header("Enemies")]
        [SerializeField] private EnemyPositions[] _enemySpawnPositions;
        [SerializeField] private List<EnemyController> _wave1;
        [SerializeField] private List<EnemyController> _wave2;
        [SerializeField] private ColliderTrigger _trigger;

        private bool _spawn;
        private List<EnemyController>[] _waves;
        private int _spawnedEnemiesCount;

        private bool _isLoading;

        public bool IsOxygenWasting => _isOxygenWasting;

        public EventHandler OnStartSpawnWaves;
        public EventHandler OnEndSpawnWaves;
        public EventHandler OnStartLoadingSection;

        public static SectionTutorial Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (_startDoor != null)
                _startDoor.Initialize(Door.DoorType.Start);

            if (_endDoor != null)
                _endDoor.Initialize(Door.DoorType.End);

            PlayerController.Instance.Visual.OnFinishDeathAnimation += PlayerVisual_OnFinishDeathAnimation;
            FadeScreen.Instance.OnEndFade += Fade_OnEndFade;

            if (_giveChip)
                ChipRewardUI.Instance.OnPlayerChooseChip += ChipRewardUI_OnPlayerChooseChip;

            if (_trigger != null)
                _trigger.OnPlayerEnterTrigger += Trigger_OnPlayerEnterTrigger;

            if (_startWaveOnAwake)
                SpawnWaves();
        }

        private void PlayerVisual_OnFinishDeathAnimation(object sender, EventArgs e)
        {
            if (_isFinalSection)
            {
                CompleteTutorial(false);
            }
            else
            {
                UIController.Instance.PushScreen(ResultScreen.Instance);
                MyGameManager.ClearEssence();
            }
        }

        public void CompleteTutorial(bool needDelay = true)
        {
            StartCoroutine(CompleteTutorialRoutine(needDelay));
        }

        private IEnumerator CompleteTutorialRoutine(bool needDelay)
        {
            if (needDelay)
                yield return new WaitForSeconds(3f);

            PlayerPrefsController.SetBool(PlayerPrefsController.TUTORIAL_COMPLETE, true);
            UIController.Instance.PushScreen(FadeScreen.Instance);
        }

        private void Fade_OnEndFade(object sender, EventArgs e)
        {
            PlayerPrefsController.CleanRunInfo();
            StartCoroutine(EnterNextSectionRoutine());
        }

        private void Trigger_OnPlayerEnterTrigger(object sender, EventArgs e)
        {
            if (_spawn)
                return;
            _spawn = true;
            SpawnWaves();
        }

        public void SpawnWaves()
            => StartCoroutine(SpawnWavesRoutine());

        private IEnumerator SpawnWavesRoutine()
        {
            OnStartSpawnWaves?.Invoke(this, EventArgs.Empty);

            _waves = new List<EnemyController>[] { _wave1, _wave2 };
            int currentWave = 0;
            while (currentWave < _waves.Length && _waves[currentWave] != null && _waves[currentWave].Count != 0)
            {
                SoundManager.Instance?.PlaySound(_startWaveSound);
                SpawnWave(currentWave);
                yield return new WaitUntil(() => _spawnedEnemiesCount == 0);
                currentWave++;
            }
            yield return new WaitForSeconds(1.5f);
            OnEndSpawnWaves?.Invoke(this, EventArgs.Empty);
        }

        private void SpawnWave(int waveIndex)
        {
            for (int i = 0; i < _waves[waveIndex].Count; i++)
            {
                Vector2 initialPosition = _enemySpawnPositions[i % _enemySpawnPositions.Length][0].transform.position;
                GameObject enemy = Instantiate(_waves[waveIndex][i], initialPosition, Quaternion.identity).gameObject;
                if (enemy.TryGetComponent(out AINavigation navigation))
                    navigation.Initialize(_enemySpawnPositions[i % _enemySpawnPositions.Length].Array);
                _spawnedEnemiesCount++;
            }
        }

        public void ReduseSpawnedEnemiesCount()
        {
            _spawnedEnemiesCount--;
        }

        public void EnterNextSection()
        {
            if (_isLoading)
                return;

            if (_giveChip)
                UIController.Instance.PushScreen(ChipRewardUI.Instance);
            else
                StartCoroutine(EnterNextSectionRoutine());
        }

        private void ChipRewardUI_OnPlayerChooseChip(object sender, EventArgs e)
        {
            if (_isLoading)
                return;
            StartCoroutine(EnterNextSectionRoutine());
        }

        private IEnumerator EnterNextSectionRoutine()
        {
            OnStartLoadingSection?.Invoke(this, EventArgs.Empty);
            yield return new WaitForFixedUpdate();
            _isLoading = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            _isLoading = false;
        }
    }
}
