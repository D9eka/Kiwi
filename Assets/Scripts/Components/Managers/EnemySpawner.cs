using Creatures.Enemy;
using System.Collections.Generic;
using System.Linq;

public static class EnemySpawner
{
    public static List<EnemyController>[] GetWaves(int spawnPoints, int wavesCount, EnemyController[] enemies, int maxEnemiesCount)
    {
        int minWavePoints = enemies.Select(enemy => enemy.SpawnPointPrice).Min();
        int[] wavePoints = GetWavesPoints(spawnPoints, wavesCount, minWavePoints);
        List<EnemyController>[] waves = new List<EnemyController>[wavesCount];
        for (int i = 0; i < wavesCount; i++)
        {
            waves[i] = GetWave(wavePoints[0], enemies, maxEnemiesCount);
        }
        return waves;
    }

    private static int[] GetWavesPoints(int spawnPoints, int wavesCount, int minWavePoints)
    {
        int availableWavePoints = spawnPoints;
        int[] wavePoints = new int[wavesCount];
        for (int i = 0; i < wavesCount - 1; i++)
        {
            int maxWavePoint = availableWavePoints - ((wavesCount - i - 1) * minWavePoints);
            int wavePoint = UnityEngine.Random.Range(minWavePoints, maxWavePoint + 1);
            wavePoints[i] = wavePoint;
            availableWavePoints -= wavePoint;
        }
        wavePoints[^1] = availableWavePoints;
        return wavePoints;
    }

    private static List<EnemyController> GetWave(int spawnPoints, EnemyController[] enemies, int maxEnemiesCount)
    {
        List<EnemyController> wave = new();
        int availableSpawnPoints = spawnPoints;
        int minSpawnPoint = enemies.Select(enemy => enemy.SpawnPointPrice).Min();
        for (int i = 0; i < maxEnemiesCount - 1; i++)
        {
            int maxSpawnPoints = availableSpawnPoints - ((maxEnemiesCount - i - 1) * minSpawnPoint);
            List<EnemyController> availableEnemies = enemies.Where(enemy => enemy.SpawnPointPrice <= maxSpawnPoints).ToList();
            EnemyController enemy = Randomiser.GetRandomElement(availableEnemies);
            wave.Add(enemy);
            availableSpawnPoints -= enemy.SpawnPointPrice;
        }
        List<EnemyController> lastAvailableEnemies = enemies.Where(enemy => enemy.SpawnPointPrice <= availableSpawnPoints).ToList();
        EnemyController lastEnemy = Randomiser.GetRandomElement(lastAvailableEnemies);
        wave.Add(lastEnemy);
        return wave;
    }
}