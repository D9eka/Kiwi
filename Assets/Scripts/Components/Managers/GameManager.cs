using System;
using Sections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool _wasKeyCardGenerated;
    public int EssenceCount { get; private set; }
    public int KeyCardCount { get; private set; }

    public int Gravity { get; private set; } = 1;

    public event EventHandler OnEssenceCountChanged;
    public event EventHandler OnGravityInverted;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void GetEssence(int count)
    {
        EssenceCount += count;
        OnEssenceCountChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool TrySpendEssence(int count)
    {
        if (!CanSpendEssence(count)) return false;
        EssenceCount -= count;
        OnEssenceCountChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public bool CanSpendEssence(int count)
    {
        return count <= EssenceCount;
    }

    public void GetKeyCard(int count)
    {
        KeyCardCount += count;
        _wasKeyCardGenerated = true;
    }

    public bool TryUseKeyCard(int count)
    {
        if (count > EssenceCount) return false;
        KeyCardCount -= count;
        return true;
    }

    public void InvertGravity()
    {
        Gravity *= -1;
        OnGravityInverted?.Invoke(this, EventArgs.Empty);
    }
}