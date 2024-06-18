using System.Collections.Generic;

public class HealthChip : PassiveChip
{
    private float _healthIncrease = 5;
    private readonly List<float> _healthIncreaseLevels = new List<float> { 5, 10, 15 };

    protected override void Upgrade()
    {
        currentLevel += 1;
        SetValues();
        StatsModifier.ModifyHealthAdder(-_healthIncreaseLevels[currentLevel]);
    }

    protected override void SetValues()
    {
        _healthIncrease = _healthIncreaseLevels[currentLevel - 1];
    }

    public override void Activate()
    {
        StatsModifier.ModifyHealthAdder(_healthIncrease);
    }

    public override void Deactivate()
    {
        StatsModifier.ModifyHealthAdder(-_healthIncrease);
    }

    public HealthChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = _healthIncreaseLevels.Count;
    }

    public HealthChip()
    {
        maxLevel = _healthIncreaseLevels.Count;
    }
}