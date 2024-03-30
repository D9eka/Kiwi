using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDamageChip : PassiveChip
{
    private const int MAX_LEVEL = 2;

    private float _dashDamageIncrease = 2f;


    protected override void SetValues()
    {
        _dashDamageIncrease = currentLevel switch
        {
            1 => 2,
            2 => 4f,
            _ => _dashDamageIncrease
        };
    }

    public override void Activate()
    {
        StatsModifier.dashDamageAdder += _dashDamageIncrease;
    }

    public override void Deactivate()
    {
        StatsModifier.dashDamageAdder -= _dashDamageIncrease;
    }

    public DashDamageChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }

    public DashDamageChip()
    {
        maxLevel = MAX_LEVEL;
    }
}