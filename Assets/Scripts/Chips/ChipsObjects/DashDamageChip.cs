using System.Collections.Generic;

public class DashDamageChip : PassiveChip
{
    private float _dashDamageIncrease = 2f;
    private readonly List<float> _dashDamageIncreaseLevels = new List<float> { 2, 4 };


    protected override void SetValues()
    {
        _dashDamageIncrease = _dashDamageIncreaseLevels[currentLevel - 1];
    }

    public override void Activate()
    {
        StatsModifier.DashDamageAdder += _dashDamageIncrease;
    }

    public override void Deactivate()
    {
        StatsModifier.DashDamageAdder -= _dashDamageIncrease;
    }

    public DashDamageChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = _dashDamageIncreaseLevels.Count;
    }

    public DashDamageChip()
    {
        maxLevel = _dashDamageIncreaseLevels.Count; ;
    }
}