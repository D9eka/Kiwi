using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int EssenceCount { get; private set; }
    public int KeyCardCount { get; private set; }
    public float Oxygen { get; private set; }
    public float MaxOxygen { get; private set; }
    public int Gravity { get; private set; } = 1;
    public event EventHandler OnEssenceCountChanged;
    public event EventHandler OnKeyCardCountChanged;
    public event EventHandler OnGravityInverted;
    public event EventHandler OnOxygenChanged;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }


    public void GetEssence(int count)
    {
        EssenceCount += count;
        OnEssenceCountChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool TrySpendEssence(int count)
    {
        if (count > EssenceCount) return false;
        EssenceCount -= count;
        OnEssenceCountChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public void GetKeyCard(int count)
    {
        KeyCardCount += count;
        OnKeyCardCountChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool TryUseKeyCard(int count)
    {
        if (count > EssenceCount) return false;
        KeyCardCount -= count;
        OnKeyCardCountChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public void InvertGravity()
    {
        Gravity *= -1;
        OnGravityInverted?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeOxygenValue(float value)
    {
        Oxygen += value;
        Oxygen = Math.Min(MaxOxygen, Oxygen);
        OnOxygenChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeOxygenPercent(float percent)
    {
        Oxygen += percent * MaxOxygen;
        Oxygen = Math.Min(MaxOxygen, Oxygen);
        OnOxygenChanged?.Invoke(this, EventArgs.Empty);
    }
}