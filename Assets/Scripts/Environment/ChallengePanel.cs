using Sections;
using UnityEngine;

public class ChallengePanel : MonoBehaviour
{
    private bool _wasUsed;

    public void TryActivateChallenge()
     {
        if (_wasUsed) 
            return;
        Section.Instance.SpawnWaves(3);
        _wasUsed = true;
    }
}