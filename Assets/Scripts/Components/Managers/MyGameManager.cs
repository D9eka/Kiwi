using Creatures.Player;
using System;
using UnityEngine;

public static class MyGameManager
{
    private static int _essenceCount = PlayerPrefsController.GetInt(ESSENCE_SAVE_KEY, 0);
    public static int EssenceCount
    {
        get
        {
            return PlayerPrefsController.GetInt(ESSENCE_SAVE_KEY, _essenceCount);
        }
        private set
        {
            EssenceEarned = value - _essenceCount > 0 ? value - _essenceCount : EssenceEarned;
            _essenceCount = value;
            PlayerPrefsController.SetInt(ESSENCE_SAVE_KEY, _essenceCount);
            OnEssenceCountChanged?.Invoke(null, EventArgs.Empty);
        }
    }
    public static bool WasKeyCardGenerated;
    public static int KeyCardCount { get; private set; }
    public static int Gravity { get; private set; } = 1;

    public static float StartTime = Time.time;
    public static int EnemyDefeated = 0;
    public static int RoomPassed = 0;
    public static int EssenceEarned = 0;
    public static int UpdatesCreated = 0;
    public static int AmountDamage = 0;
    public static int MaxDamage = 0;
    public static int DamageEarned = 0;
    public static Sprite LastDamageEnemy;
    public static int DamageHealed = 0;

    private const string ESSENCE_SAVE_KEY = "GameManagerEssence";
    public const int CHANGE_REWARD_COST = 10;
    public const int UPDATE_CHIP_COST = 25;

    public static event EventHandler OnEssenceCountChanged;
    public static event EventHandler OnGravityInverted;

    public static void GetEssence(int count)
    {
        EssenceCount += count;
    }

    public static bool TrySpendEssence(int count)
    {
        if (!CanSpendEssence(count))
            return false;
        EssenceCount -= count;
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

    public static void AddAmountDamage(float damage)
    {
        int roundedDamage = Mathf.RoundToInt(damage);
        if (roundedDamage > MaxDamage)
            MaxDamage = roundedDamage;
        AmountDamage += roundedDamage;
    }

    public static void AddEarnedDamage(Sprite enemySprite, float damage)
    {
        LastDamageEnemy = enemySprite;
        DamageEarned += Mathf.RoundToInt(damage);
    }

    public static void ClearEssence()
    {
        EssenceCount = 0;
    }
}