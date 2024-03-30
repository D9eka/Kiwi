using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldChip : UpdatingChip
{
    private const int MAX_LEVEL = 2;

    private float _blockChance = 0.2f;
    private float _cooldown = 10;
    private float _timeToActivation;
    private bool _isShieldRaised;

    protected override void SetValues()
    {
        switch (currentLevel)
        {
            case 1:
                _blockChance = 0.2f;
                break;
            case 2:
                _blockChance = 0.25f;
                break;
            case 3:
                _blockChance = 0.3f;
                break;
        }
    }

    public override void Update()
    {
        TryRaiseShield();
    }

    public ShieldChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }

    public ShieldChip()
    {
        maxLevel = MAX_LEVEL;
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