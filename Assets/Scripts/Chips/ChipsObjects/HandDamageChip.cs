using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDamageChip : PassiveChip
{
    private const int MAX_LEVEL = 3;

    private float _handDamageIncrease = 2;


    protected override void SetValues()
    {
        _handDamageIncrease = currentLevel switch
        {
            1 => 2,
            2 => 3,
            3 => 4,
            _ => _handDamageIncrease
        };
    }

    public override void Activate()
    {
        StatsModifier.handDamageAdder += _handDamageIncrease;
    }

    public override void Deactivate()
    {
        StatsModifier.handDamageAdder -= _handDamageIncrease;
    }

    public HandDamageChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }

    public HandDamageChip()
    {
        maxLevel = MAX_LEVEL;
    }
}