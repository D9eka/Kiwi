using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengePanel : MonoBehaviour
{
    private bool _wasUsed;

    public void TryActivateChallenge()
    {
        if (_wasUsed) return;
        EnemySpawner.Instance.SpawnEnemies();
        _wasUsed = true;
    }
}