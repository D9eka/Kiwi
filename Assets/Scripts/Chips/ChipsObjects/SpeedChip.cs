using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChip : PassiveChip
{
    private const int MAX_LEVEL = 3;

    private float _speedIncreasePercent= 0.1f;

    protected override void SetValues()
    {
        _speedIncreasePercent = currentLevel switch
        {
            1 => 0.1f,
            2 => 0.2f,
            3 => 0.3f,
            _ => _speedIncreasePercent
        };
    }

    public override void Activate()
    {
        StatsModifier.speedMultiplier += _speedIncreasePercent;
    }

    public override void Deactivate()
    {
        StatsModifier.speedMultiplier -= _speedIncreasePercent;
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