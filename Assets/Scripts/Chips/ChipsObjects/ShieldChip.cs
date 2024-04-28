using System.Collections.Generic;
using UnityEngine;

public class ShieldChip : UpdatingChip
{
    private float _blockChance = 0.2f;
    private readonly List<float> _blockChanceLevels = new List<float> { 0.2f, 0.25f, 0.3f };
    private float _cooldown = 10;
    private float _timeToActivation;
    private bool _isShieldRaised;

    protected override void SetValues()
    {
        _blockChance = _blockChanceLevels[currentLevel - 1];
    }

    public override void Update()
    {
        TryRaiseShield();
    }

    public ShieldChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = _blockChanceLevels.Count;
    }

    public ShieldChip()
    {
        maxLevel = _blockChanceLevels.Count;
    }

    private void TryRaiseShield()
    {
        if (_isShieldRaised) return;
        if (_timeToActivation <= 0) _isShieldRaised = true;
        else _timeToActivation -= Time.deltaTime;
    }

    public bool TryBlock()
    {
        var isSucceed = _isShieldRaised && Randomiser.Succeed(_blockChance);
        if (isSucceed) LowerShield();
        return isSucceed;
    }

    public void LowerShield()
    {
        if (!_isShieldRaised) return;
        _isShieldRaised = false;
        _timeToActivation = _cooldown;
    }
}