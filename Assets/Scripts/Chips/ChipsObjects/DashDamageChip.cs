using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDamageChip : PassiveChip
{
    private float _dashDamageIncrease = 2f;
    private readonly List<float> _dashDamageIncreaseLevels = new() { 2, 4 };


    protected override void SetValues()
    {
        _dashDamageIncrease = _dashDamageIncreaseLevels[currentLevel - 1];
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
        maxLevel = _dashDamageIncreaseLevels.Count;
    }

    public DashDamageChip()
    {
        maxLevel = _dashDamageIncreaseLevels.Count;;
    }
}