namespace PermanentBuffs.Objects
{
    public class HealthPermanentBuff : PermanentBuff
    {
        private const float MODIFIER = 10f;

        public override void Activate()
        {
            StatsModifier.ModifyHealthAdder(MODIFIER);
        }

        public override void Deactivate()
        {
            StatsModifier.ModifyHealthAdder(-MODIFIER);
        }
    }
}
