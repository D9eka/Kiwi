using Creatures.AI;
using Creatures.Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sections
{
    public class Section : MonoBehaviour
    {
        [SerializeField] private SectionSO _data;
        [Space]
        [Header("Doors")]
        [SerializeField] private Door _startDoor;
        [SerializeField] private Door _endDoor;
        [SerializeField] private Door _secretDoor;
        [Header("Enemies")]
        [SerializeField] EnemyController[] _enemies;
        [SerializeField] EnemyPositions[] _enemySpawnPositions;

        private int _spawnPoints;
        private List<EnemyController>[] _waves;
        private int _spawnedEnemiesCount;

        public SectionSO Data => _data;
        public SectionTypeSO TypeSO => _data.SectionTypeSO;
        public Door SecretDoor => _secretDoor;

        public EventHandler OnStartSpawnWaves;
        public EventHandler OnEndSpawnWaves;
        public static Section Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            InitializeDoor(_startDoor, Door.DoorType.Start);
            InitializeDoor(_endDoor, Door.DoorType.End);
            InitializeDoor(_secretDoor, Door.DoorType.Secret);
        }

        private void InitializeDoor(Door door, Door.DoorType doorType)
        {
            if (door != null)
                door.Initialize(doorType);
        }

        private void Start()
        {
            SpawnWaves();
            SoundManager.Instance?.PlayMusic(_data.SectionTypeSO.BackgroundType);
        }

        public void SpawnWaves()
            => SpawnWaves(_data.SectionTypeSO.WavesCount);

        public void SpawnWaves(int wavesCount)
            => StartCoroutine(SpawnWavesRoutine(wavesCount));

        private IEnumerator SpawnWavesRoutine(int wavesCount)
        {
            yield return new WaitForSeconds(1f);
            if (_data.SectionTypeSO.SectionType == SectionType.Boss)
                yield break;
            if (SectionManager.Instance.NeedStartWave && _data.SectionTypeSO.WavesCount > 0)
            {
                _spawnPoints = SectionManager.Instance.GetSpawnPoints(_data.SectionTypeSO);
                _waves = EnemySpawner.GetWaves(_spawnPoints, wavesCount, _enemies, _enemySpawnPositions.Length);

                OnStartSpawnWaves?.Invoke(this, EventArgs.Empty);
                int currentWave = 0;
                while (currentWave < _waves.Length)
                {
                    SoundManager.Instance.PlaySound(SoundManager.Instance._startWaveSound);
                    SpawnWave(currentWave);
                    yield return new WaitUntil(() => _spawnedEnemiesCount == 0);
                    currentWave++;
                }
            }
            OnEndSpawnWaves?.Invoke(this, EventArgs.Empty);
        }

        private void SpawnWave(int waveIndex)
        {
            for (int i = 0; i < _waves[waveIndex].Count; i++)
            {
                Vector2 initialPosition = _enemySpawnPositions[i][0].transform.position;
                GameObject enemy = Instantiate(_waves[waveIndex][i], initialPosition, Quaternion.identity).gameObject;
                if (enemy.TryGetComponent(out AINavigation navigation))
                    navigation.Initialize(_enemySpawnPositions[i].Array);
                _spawnedEnemiesCount++;
            }
        }

        public void ReduseSpawnedEnemiesCount()
        {
            _spawnedEnemiesCount--;
        }
    }
}

[Serializable]
public class EnemyPositions
{
    [SerializeField] private GameObject[] _positions;

    public GameObject this[int key]
    {
        get
        {
            return _positions[key];
        }
        set
        {
            _positions[key] = value;
        }
    }

    public GameObject[] Array => _positions;
}
