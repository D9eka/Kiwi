using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChip : PassiveChip
{
    private const int MAX_LEVEL = 3;

    private float _healthIncrease = 5;

    protected override void SetValues()
    {
        _healthIncrease = currentLevel switch
        {
            1 => 5,
            2 => 10,
            3 => 15,
            _ => _healthIncrease
        };
    }

    public override void Activate()
    {
        StatsModifier.healthAdder += _healthIncrease;
    }

    public override void Deactivate()
    {
        StatsModifier.healthAdder -= _healthIncrease;
    }

    public HealthChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }

    public HealthChip()
    {
        maxLevel = MAX_LEVEL;
    }
}