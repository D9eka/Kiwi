public static class StatsModifier
{
    // Задумка в том, чтобы в коде, где использвуются характеристики, прибавлять/умножать на эти значения.
    // Текущие значения регулируются раличными чипами, а может и не только
    public static int brokenChipsCount = 0;
    public static float healthAdder = 0;
    public static float dashDamageAdder = 0;
    public static float handDamageAdder = 0;
    public static float bulletDamageAdder = 0;
    public static float damageMultiplier = 1;
    public static float takenDamageMultiplier = 1;
    public static float speedMultiplier = 1;
    public static float priceMultiplier = 1;
    public static float brokenChipDamagePercentAdder = 0;

    public static float GetModifiedDamage(float damage, DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Hand:
                damage += handDamageAdder;
                break;
            case DamageType.Bullet:
                damage += bulletDamageAdder;
                break;
            case DamageType.Dash:
                damage += dashDamageAdder;
                break;
        }
 
        damage = damage * damageMultiplier * (1 + brokenChipDamagePercentAdder * brokenChipsCount);
        return damage;
    }

    public static float GetModifiedSpeed(float speed)
    {
        speed *= speedMultiplier;
        return speed;
    }

    public static float GetModifiedPrice(float price)
    {
        price *= priceMultiplier;
        return price;
    }

    public static float GetModifiedTakenDamage(float takenDamage)
    {
        takenDamage *= takenDamageMultiplier;
        return takenDamage;
    }
}