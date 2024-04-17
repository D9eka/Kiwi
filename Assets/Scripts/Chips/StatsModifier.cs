using Components.Health;
using Components.Oxygen;
using Creatures.Player;

public static class StatsModifier
{
    private static float _healthAdder = 0;
    private static float _oxygenAdder = 0;

    public static int BrokenChipsCount = 0;

    public static int DashCount = 0;
    public static float DashDamageAdder = 0;

    public static float HandDamageAdder = 0;
    public static float BulletDamageAdder = 0;
    public static float DamageMultiplier = 1;
    public static float TakenDamageMultiplier = 1;

    public static float SpeedMultiplier = 1;
    public static float PriceMultiplier = 1;

    public static float BrokenChipDamagePercentAdder = 0;

    public static bool CanChangeReward = false;

    public static float GetModifiedDamage(float damage, DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Hand:
                damage += HandDamageAdder;
                break;
            case DamageType.Bullet:
                damage += BulletDamageAdder;
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

    public static float GetModifiedPrice(float price)
    {
        price *= PriceMultiplier;
        return price;
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
}