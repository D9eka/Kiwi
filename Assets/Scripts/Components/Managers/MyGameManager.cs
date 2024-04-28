using System;
using System.Diagnostics;

public static class MyGameManager
{
    public static bool WasKeyCardGenerated;
    public static int EssenceCount { get; private set; }
    public static int KeyCardCount { get; private set; }

    public static int Gravity { get; private set; } = 1;

    public static event EventHandler OnEssenceCountChanged;
    public static event EventHandler OnGravityInverted;

    public static void GetEssence(int count)
    {
        EssenceCount += count;
        OnEssenceCountChanged?.Invoke(null, EventArgs.Empty);
    }

    public static bool TrySpendEssence(int count)
    {
        if (!CanSpendEssence(count))
            return false;
        EssenceCount -= count;
        OnEssenceCountChanged?.Invoke(null, EventArgs.Empty);
        return true;
    }

    public static bool CanSpendEssence(int count)
    {
        return count <= EssenceCount;
    }

    public static void GetKeyCard(int count)
    {
        KeyCardCount += count;
        WasKeyCardGenerated = true;
    }

    public static bool TryUseKeyCard(int count)
    {
        if (count > KeyCardCount) return false;
        KeyCardCount -= count;
        return true;
    }

    public static void InvertGravity()
    {
        Gravity *= -1;
        OnGravityInverted?.Invoke(null, EventArgs.Empty);
    }
}