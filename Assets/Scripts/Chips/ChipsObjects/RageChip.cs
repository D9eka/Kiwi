using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageChip : PassiveChip
{
    private float _damageIncreasePercent = 0.4f;
    private float _takenDamageIncreasePercent = 0.6f;
    private readonly List<float> _damageIncreasePercentLevels = new List<float> { 0.4f, 0.5f, 0.6f };
    private readonly List<float> _takenDamageIncreasePercentLevels = new List<float> { 0.6f, 0.5f, 0.4f };


    protected override void SetValues()
    {
        _damageIncreasePercent = _damageIncreasePercentLevels[currentLevel - 1];
        _takenDamageIncreasePercent = _takenDamageIncreasePercentLevels[currentLevel - 1];
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
        maxLevel = _damageIncreasePercentLevels.Count;
    }

    public RageChip()
    {
        maxLevel = _damageIncreasePercentLevels.Count;
    }
}