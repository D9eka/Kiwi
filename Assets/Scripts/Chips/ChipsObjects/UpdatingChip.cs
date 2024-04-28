public abstract class UpdatingChip : Chip
{
    public abstract void Update();
    protected UpdatingChip()
    {
        currentLevel = 1;
        SetValues();
    }

    protected UpdatingChip(int currentLevel)
    {
        this.currentLevel = currentLevel;
        SetValues();
    }
}