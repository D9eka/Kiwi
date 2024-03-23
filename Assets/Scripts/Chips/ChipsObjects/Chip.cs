using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Chip
{
    protected int maxLevel;
    protected int currentLevel;

    public int MaxLevel => maxLevel;
    public int CurrentLevel => currentLevel;

    private bool CanBeUpgrade => currentLevel < maxLevel;

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

    public void Upgrade()
    {
        Deactivate();
        currentLevel += 1;
        SetValues();
        Activate();
    }

    public void TryUpgrade()
    {
        if (CanBeUpgrade)
        {
            Upgrade();
        }
    }

    protected abstract void SetValues();
    public abstract void Activate();
    public abstract void Deactivate();
}