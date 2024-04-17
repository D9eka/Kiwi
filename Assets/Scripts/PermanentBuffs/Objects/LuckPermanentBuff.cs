namespace PermanentBuffs.Objects
{
    public class LuckPermanentBuff : PermanentBuff
    {
        public override void Activate()
        {
            StatsModifier.CanChangeReward = true;
        }

        public override void Deactivate()
        {
            StatsModifier.CanChangeReward = false;
        }
    }
}
