using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageChip : PassiveChip
{
    private const int MAX_LEVEL = 3;

    private float _damageIncreasePercent = 0.4f;

    private float _takenDamageIncreasePercent = 0.6f;


    protected override void SetValues()
    {
        switch (currentLevel)
        {
            case 1:
                _damageIncreasePercent = 0.4f;
                _takenDamageIncreasePercent = 0.6f;
                break;
            case 2:
                _damageIncreasePercent = 0.5f;
                _takenDamageIncreasePercent = 0.5f;
                break;
            case 3:
                _damageIncreasePercent = 0.6f;
                _takenDamageIncreasePercent = 0.4f;
                break;
        }
    }

    public override void Activate()
    {
        StatsModifier.damageMultiplier += _damageIncreasePercent;
        StatsModifier.takenDamageMultiplier += _takenDamageIncreasePercent;
    }

    public override void Deactivate()
    {
        StatsModifier.damageMultiplier -= _damageIncreasePercent;
        StatsModifier.takenDamageMultiplier -= _takenDamageIncreasePercent;
    }

    public RageChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }

    public RageChip()
    {
        maxLevel = MAX_LEVEL;
    }
}