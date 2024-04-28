public class BrokenChip : PassiveChip
{
    private const int MAX_LEVEL = 1;

    protected override void SetValues()
    {
    }

    public override void Activate()
    {
        StatsModifier.BrokenChipsCount += 1;
    }

    public override void Deactivate()
    {
        StatsModifier.BrokenChipsCount -= 1;
    }

    public BrokenChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }

    public BrokenChip()
    {
        maxLevel = MAX_LEVEL;
    }
}