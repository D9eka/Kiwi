using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveChip : Chip
{
    public abstract void Activate();
    public abstract void Deactivate();
    protected List<List<float>> values;

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