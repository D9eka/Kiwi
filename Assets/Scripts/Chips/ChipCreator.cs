public static class ChipCreator
{
    public static Chip Create(ChipType chipType)
    {
        return chipType switch
        {
            ChipType.Discount => new DiscountChip(),
            ChipType.Health => new HealthChip(),
            ChipType.Rage => new RageChip(),
            ChipType.Shield => new ShieldChip(),
            ChipType.Speed => new SpeedChip(),
            _ => null
        };
    }
}