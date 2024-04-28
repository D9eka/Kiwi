using System.Collections.Generic;

public class BulletDamageChip : PassiveChip
{
    private float _bulletDamageIncrease = 2;
    private readonly List<float> _bulletDamageIncreaseLevels = new List<float> { 2, 3, 4 };


    protected override void SetValues()
    {
        _bulletDamageIncrease = _bulletDamageIncreaseLevels[currentLevel - 1];
    }

    public override void Activate()
    {
        StatsModifier.BulletDamageAdder += _bulletDamageIncrease;
    }

    public override void Deactivate()
    {
        StatsModifier.BulletDamageAdder -= _bulletDamageIncrease;
    }

    public BulletDamageChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = _bulletDamageIncreaseLevels.Count;
    }

    public BulletDamageChip()
    {
        maxLevel = _bulletDamageIncreaseLevels.Count;
    }
}