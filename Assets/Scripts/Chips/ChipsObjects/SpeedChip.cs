using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChip : PassiveChip
{
    private float _speedIncreasePercent = 0.1f;
    private readonly List<float> _speedIncreasePercentLevels = new List<float> { 0.1f, 0.2f, 0.3f };


    protected override void SetValues()
    {
        _speedIncreasePercent = _speedIncreasePercentLevels[currentLevel - 1];
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
        maxLevel = _speedIncreasePercentLevels.Count;
    }


    public SpeedChip()
    {
        maxLevel = _speedIncreasePercentLevels.Count;
    }
}