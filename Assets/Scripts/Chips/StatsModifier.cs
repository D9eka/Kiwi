public static class StatsModifier
{
    // Задумка в том, чтобы в коде, где использвуются характеристики, прибавлять/умножать на эти значения.
    // Текущие значения регулируются раличными чипами, а может и не только
    public static float healthAdder = 0;
    public static float damageMultiplier = 1;
    public static float takenDamageMultiplier = 1;
    public static float speedDamageMultiplier = 1;
    public static float pricesMultiplier = 1;
}