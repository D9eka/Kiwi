public abstract class PassiveChip : Chip
{
    public abstract void Activate();
    public abstract void Deactivate();

    protected override void Upgrade()
    {
        Deactivate();
        currentLevel += 1;
        SetValues();
        Activate();
    }

    protected PassiveChip()
    {
        currentLevel = 1;
        SetValues();
    }

    protected PassiveChip(int currentLevel)
    {
        this.currentLevel = currentLevel;
        SetValues();
    }
}