using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirismChip : Chip
{
    private const int MAX_LEVEL = 3;

    private float _chance = 0.02f;
    private float _restorableHealth = 2;


    protected override void SetValues()
    {
        switch (currentLevel)
        {
            case 1:
                _chance = 0.02f;
                _restorableHealth = 2;
                break;
            case 2:
                _chance = 0.04f;
                _restorableHealth = 4;
                break;
            case 3:
                _chance= 0.06f;
                _restorableHealth = 6;
                break;
        }
    }

    public VampirismChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }

    public VampirismChip()
    {
        maxLevel = MAX_LEVEL;
    }

    public void TryRestoreHealth()
    {
        if (Randomiser.Succeed(_chance))
        {
            //playerHealth.Heal(restorabelHealth)
        }
    }
}