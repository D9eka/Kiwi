using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Components.Health;
using Creatures.AI;
using Sections;
using UnityEngine;

public class EnemyStaticSpawner : MonoBehaviour
{
    [SerializeField] private int _wavesCount;
    private int _currentWave;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<EnemySO> _enemies;
    [SerializeField] private List<int> spawnController;
    private SectionType _sectionType;
    private int _spawnIndex;
    private int _enemyIndex;
    private float _waitingTimeCooldown = 0.5f;
    private IEnumerator _coroutine;
    [SerializeField] private bool spawnAtStart;

    public int DefeatedEnemiesCount { get; private set; }
    public bool AllEnemiesDefeated => DefeatedEnemiesCount == _enemies.Count;

    [ContextMenu(nameof(OnEnemyDeath))]
    private void OnEnemyDeath()
    {
        DefeatedEnemiesCount += 1;
        if (IsWaveEnded())
        {
            if (AllEnemiesDefeated) CurrentSectionManager.Instance.EndBattle();
            else SpawnEnemies();
        }
    }

    private void Start()
    {
        _sectionType = SectionManager.Instance.CurrentSectionType.SectionType;
        Debug.Log(_sectionType);
        if (spawnAtStart)
        {
            SpawnEnemies();
        }
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        _currentWave += 1;
        if (_currentWave > _wavesCount) StopCoroutine(SpawnEnemiesCoroutine());
        foreach (var spawnPoint in _spawnPoints)
        {
            var navigationPoints = spawnPoint.GetComponentInChildren<NavigationPoints>().Points;
            for (var i = 0; i < spawnController[_spawnIndex]; i++)
            {
                yield return new WaitForSeconds(_waitingTimeCooldown);
                var enemy = Instantiate(_enemies[_enemyIndex].EnemyPrefab, spawnPoint);
                enemy.GetComponent<HealthComponent>()._onDie.AddListener(OnEnemyDeath);
                enemy.transform.localPosition = Vector3.zero;
                if (enemy.TryGetComponent(out AINavigation aiNavigation))
                {
                    aiNavigation.SetNavigationPoints(navigationPoints);
                }

                _enemyIndex += 1;
            }

            _spawnIndex += 1;
        }

        // _currentWave += 1;
    }

    public void SpawnEnemies()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private bool IsWaveEnded()
    {
        var allWavesEnemyCount = 0;
        for (var i = 0; i < _currentWave; i++)
        {
            for (var j = 0; j < _spawnPoints.Count; j++)
            {
                allWavesEnemyCount += spawnController[i * _spawnPoints.Count + j];
            }
        }

        return allWavesEnemyCount == DefeatedEnemiesCount;
    }
}