using System;


public abstract class Chip
{
    protected int maxLevel;
    protected int currentLevel;
    public ChipSO ChipSO { get; private set; }

    public int MaxLevel => maxLevel;
    public int CurrentLevel => currentLevel;

    private bool CanBeUpgrade => currentLevel < maxLevel;

    //Нужен для того, чтобы обновлять описание чипа
    public event EventHandler OnUpgraded;

    protected Chip()
    {
        currentLevel = 1;
        SetValues();
    }

    protected Chip(int currentLevel)
    {
        this.currentLevel = currentLevel;
        SetValues();
    }

    protected virtual void Upgrade()
    {
        currentLevel += 1;
        SetValues();
        OnUpgraded?.Invoke(this, EventArgs.Empty);
    }

    public void TryUpgrade()
    {
        if (CanBeUpgrade)
        {
            Upgrade();
        }
    }

    public void SetChipSO(ChipSO chipSO)
    {
        ChipSO = chipSO;
    }

    protected abstract void SetValues();
}