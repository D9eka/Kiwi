using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sections;
using UnityEngine;

public class EnemyStaticSpawner : MonoBehaviour
{
    [SerializeField] private int _wavesCount;
    private int _currentWave;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<Transform> _enemies;
    [SerializeField] private List<float> spawnController;
    private SectionType _sectionType;
    private int _spawnIndex;

    private void Start()
    {
        _sectionType = SectionManager.Instance.CurrentSectionType.SectionType;
        if (_sectionType is SectionType.Battle or SectionType.Engineer)
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        if (_currentWave > _wavesCount + 1) return;
        foreach (var spawnPoint in _spawnPoints)
        {
            for (var i = 0; i < spawnController[_spawnIndex]; i++)
            {
                Instantiate(_enemies[_spawnIndex], spawnPoint);
                _spawnIndex += 1;
            }
        }

        _currentWave += 1;
    }
}