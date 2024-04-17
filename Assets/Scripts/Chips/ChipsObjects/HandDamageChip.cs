using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDamageChip : PassiveChip
{
    private float _handDamageIncrease = 2;
    private readonly List<float> _handDamageIncreaseLevels = new List<float> { 2, 3, 4 };



    protected override void SetValues()
    {
        _handDamageIncrease = _handDamageIncreaseLevels[currentLevel - 1];
    }

    public override void Activate()
    {
        StatsModifier.HandDamageAdder += _handDamageIncrease;
    }

    public override void Deactivate()
    {
        StatsModifier.HandDamageAdder -= _handDamageIncrease;
    }

    public HandDamageChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = _handDamageIncreaseLevels.Count;
    }

    public HandDamageChip()
    {
        maxLevel = _handDamageIncreaseLevels.Count;
    }
}