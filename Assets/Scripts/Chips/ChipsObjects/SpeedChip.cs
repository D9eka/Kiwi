using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChip : Chip
{
    private const int MAX_LEVEL = 3;

    private float _speedIncreasePercent= 0.1f;

    protected override void SetValues()
    {
        switch (currentLevel)
        {
            case 1:
                _speedIncreasePercent = 0.1f;
                break;
            case 2:
                _speedIncreasePercent = 0.2f;
                break;
            case 3:
                _speedIncreasePercent = 0.3f;
                break;
        }
    }

    public override void Activate()
    {
        StatsModifier.speedDamageMultiplier += _speedIncreasePercent;
    }

    public override void Deactivate()
    {
        StatsModifier.speedDamageMultiplier -= _speedIncreasePercent;
    }

    public SpeedChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }
    

    public SpeedChip()
    {
        maxLevel = MAX_LEVEL;
    }
}