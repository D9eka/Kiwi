using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageChip : PassiveChip
{
    private const int MAX_LEVEL = 3;

    private float _bulletDamageIncrease = 2;


    protected override void SetValues()
    {
        _bulletDamageIncrease = currentLevel switch
        {
            1 => 2,
            2 => 3,
            3 => 4,
            _ => _bulletDamageIncrease
        };
    }

    public override void Activate()
    {
        StatsModifier.bulletDamageAdder += _bulletDamageIncrease;
    }

    public override void Deactivate()
    {
        StatsModifier.bulletDamageAdder -= _bulletDamageIncrease;
    }

    public BulletDamageChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }

    public BulletDamageChip()
    {
        maxLevel = MAX_LEVEL;
    }
}