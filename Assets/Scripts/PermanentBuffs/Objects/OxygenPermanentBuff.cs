namespace PermanentBuffs.Objects
{
    public class OxygenPermanentBuff : PermanentBuff
    {
        private const float MODIFIER = 60f;

        public override void Activate()
        {
            StatsModifier.ModifyOxygenAdder(MODIFIER);
        }

        public override void Deactivate()
        {
            StatsModifier.ModifyOxygenAdder(-MODIFIER);
        }
    }
}
