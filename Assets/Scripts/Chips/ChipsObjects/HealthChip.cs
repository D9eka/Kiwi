using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChip : Chip
{
    private const int MAX_LEVEL = 3;

    private float _healthIncrease = 5;

    protected override void SetValues()
    {
        switch (currentLevel)
        {
            case 1:
                _healthIncrease = 5;
                break;
            case 2:
                _healthIncrease = 10;
                break;
            case 3:
                _healthIncrease = 15;
                break;
        }
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