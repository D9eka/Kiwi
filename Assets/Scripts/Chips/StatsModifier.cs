using Components.Health;
using Components.Oxygen;
using Creatures.Player;
using System;
using UnityEngine;

public static class StatsModifier
{
    private static float _healthAdder = 0;
    private static float _oxygenAdder = 0;
    private static int _dashCount;

    public static int BrokenChipsCount = 0;
    public static int DashCount
    {
        get => _dashCount;
        set
        {
            OnChangeDashCount?.Invoke(null, value - _dashCount);
            _dashCount = value;
        }
    }
    public static float DashDamageAdder = 0;
    public static float DamageMultiplier = 1;
    public static float HandDamageAdder = 0;
    public static float BulletDamageAdder = 0;
    public static float TakenDamageMultiplier = 1;
    public static int MeleeAddHPChance = 0;

    public static float SpeedMultiplier = 1;
    public static float PriceMultiplier = 1;

    public static float BrokenChipDamagePercentAdder = 0;

    public static bool CanChangeReward = false;

    public static EventHandler<int> OnChangeDashCount;

    public static float GetModifiedDamage(float damage, DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Hand:
                damage += HandDamageAdder;
                if (UnityEngine.Random.Range(1, 99) <= MeleeAddHPChance)
                    PlayerController.Instance.GetComponent<HealthComponent>().ModifyHealth(MeleeAddHPChance);
                break;
            case DamageType.Melee:
                if (UnityEngine.Random.Range(1, 99) <= MeleeAddHPChance)
                    PlayerController.Instance.GetComponent<HealthComponent>().ModifyHealth(MeleeAddHPChance);
                break;
            case DamageType.Bullet:
                damage += BulletDamageAdder;
                break;
            case DamageType.Trap:
                break;
            case DamageType.Dash:
                damage += DashDamageAdder;
                break;
        }
        damage = damage * DamageMultiplier * (1 + BrokenChipDamagePercentAdder * BrokenChipsCount);
        return damage;
    }

    public static float GetModifiedSpeed(float speed)
    {
        speed *= SpeedMultiplier;
        return speed;
    }

    public static int GetModifiedPrice(int price)
    {
        return Mathf.RoundToInt(price * PriceMultiplier);
    }

    public static float GetModifiedTakenDamage(float takenDamage)
    {
        takenDamage *= TakenDamageMultiplier;
        return takenDamage;
    }

    public static void ModifyHealthAdder(float addingValue)
    {
        _healthAdder += addingValue;
        PlayerController.Instance.GetComponent<HealthComponent>().ChangeHealthStats(addingValue);
    }

    public static float GetModifiedHealth(float health)
    {
        health += _healthAdder;
        return health;
    }

    public static void ModifyOxygenAdder(float addingValue)
    {
        _oxygenAdder += addingValue;
        PlayerController.Instance.GetComponent<OxygenComponent>().ChangeOxygenStats(addingValue);
    }

    public static float GetModifiedOxygen(float oxygen)
    {
        oxygen += _oxygenAdder;
        return oxygen;
    }

    public static void Clear()
    {
        _healthAdder = 0;
        _oxygenAdder = 0;

        BrokenChipsCount = 0;

        DashDamageAdder = 0;
        DamageMultiplier = 1;
        HandDamageAdder = 0;
        BulletDamageAdder = 0;
        TakenDamageMultiplier = 1;
        MeleeAddHPChance = 0;

        SpeedMultiplier = 1;
        PriceMultiplier = 1;

        BrokenChipDamagePercentAdder = 0;
}
}