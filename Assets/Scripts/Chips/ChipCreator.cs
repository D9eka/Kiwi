public static class ChipCreator
{
    public static Chip Create(ChipSO chipSO)
    {
        // Chip chip;
        // switch (chipType)
        // {
        //     case ChipType.Discount:
        // }
        // var a = GetChip(chipType);
        // a.SetChipSO();
        Chip chip = chipSO.ChipType switch
        {
            ChipType.Discount => new DiscountChip(),
            ChipType.Health => new HealthChip(),
            ChipType.Rage => new RageChip(),
            ChipType.Shield => new ShieldChip(),
            ChipType.Speed => new SpeedChip(),
            ChipType.Broken => new BrokenChip(),
            ChipType.BulletDamage => new BulletDamageChip(),
            ChipType.DashDamage => new DashDamageChip(),
            ChipType.HandDamage => new HandDamageChip(),
            ChipType.Nihility => new NihilityChip(),
            ChipType.Revival => new RevivalChip(),
            ChipType.Vampirism => new VampirismChip(),
            _ => null
        };
        chip?.SetChipSO(chipSO);
        return chip;
    }
}