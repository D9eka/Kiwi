using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirismChip : Chip
{
    private const int MAX_LEVEL = 3;

    private float _chance = 0.02f;
    private float _restorableHealth = 2;
    private readonly List<float> _chanceLevels = new List<float> {0.02f, 0.04f, 0.06f};
    private readonly List<float> _restorableHealthLevels = new List<float> {2f, 4f, 6f};


    protected override void SetValues()
    {
        _chance = _chanceLevels[currentLevel];
        _restorableHealth = _restorableHealthLevels[currentLevel];
    }

    public VampirismChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = _chanceLevels.Count;
    }

    public VampirismChip()
    {
        maxLevel = _chanceLevels.Count;
    }

    public void TryRestoreHealth()
    {
        if (Randomiser.Succeed(_chance))
        {
            //playerHealth.Heal(restorabelHealth)
        }
    }
}