namespace PermanentBuffs.Objects
{
    public class DashPermanentBuff : PermanentBuff
    {
        public override void Activate()
        {
            StatsModifier.DashCount++;
        }

        public override void Deactivate()
        {
            StatsModifier.DashCount--;
        }
    }
}
