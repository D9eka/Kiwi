using System.Collections.Generic;

public class RevivalChip : Chip
{
    private float _restoringHealthPercent = 0.2f;
    private readonly List<float> _restoringHealthPercentLevels = new List<float> { 0.2f, 0.4f, 0.6f };


    protected override void SetValues()
    {
        _restoringHealthPercent = _restoringHealthPercentLevels[currentLevel - 1];
    }

    public RevivalChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = _restoringHealthPercentLevels.Count;
    }

    public RevivalChip()
    {
        maxLevel = _restoringHealthPercentLevels.Count;
    }

    public float Revive()
    {
        ChipManager.Instance.RemoveChip(this);
        ChipManager.Instance.ObtainBrokenChip();
        return _restoringHealthPercent;
    }
}