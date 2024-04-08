using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int BASE_ENEMY_POINTS = 30;
    private const int CHALLENGE_POINTS = 10;
    private int _enemyPoints;
    [SerializeField] private int _wavesCount;
    [SerializeField] private List<float> _separator;
    private int _currentWave;

    [SerializeField] private List<Transform> _spawnPoints;

    //вместо Transform будет EnemySO или что-то такое
    [SerializeField] private List<Transform> _enemies;
    private SectionType _sectionType;
    public static EnemySpawner Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _sectionType = SectionManager.Instance.CurrentSectionType.SectionType;
        TrySetAutoValues();
        TryFixSeparator();
        SetEnemyPoints();
        if (_sectionType is SectionType.Battle or SectionType.Engineer)
        {
            SpawnEnemies();
        }
    }

    private void SetEnemyPoints()
    {
        _enemyPoints = BASE_ENEMY_POINTS + (int)Math.Round(SectionManager.Instance.CurrentSectionIndex * 1.8);
        if (_sectionType is SectionType.Challenge)
            _enemyPoints += CHALLENGE_POINTS;
    }

    public void SpawnEnemies()
    {
        if (_currentWave > _wavesCount + 1) return;
        var currentEnemyPoints = (int)Math.Round(_enemyPoints * _separator[_currentWave]);
        while (currentEnemyPoints > 0)
        {
            var enemy = Randomiser.GetRandomElement(_enemies);
            if (enemy.position.x > currentEnemyPoints)
            {
                _enemies.Remove(enemy);
                continue;
            }

            var spawnPoint = Randomiser.GetRandomElement(_spawnPoints);
            Instantiate(enemy.gameObject, spawnPoint);
            currentEnemyPoints -= (int)enemy.position.x;
        }

        _currentWave += 1;
    }

    private void TrySetAutoValues()
    {
        if (_wavesCount == 0) _wavesCount = 1;
        if (_wavesCount < _separator.Count) _separator = _separator.GetRange(0, _wavesCount);
        if (_wavesCount > _separator.Count) _separator = new List<float>(_wavesCount);
    }

    private void TryFixSeparator()
    {
        var sum = _separator.Sum();
        if (sum <= 0)
        {
            for (var i = 0; i < _separator.Count; i++)
            {
                _separator[i] = 1 / _separator.Count;
            }

            return;
        }

        for (var i = 0; i < _separator.Count; i++)
        {
            _separator[i] /= sum;
        }
    }
}