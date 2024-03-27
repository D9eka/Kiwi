using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldChip : UpdatingChip
{
    private const int MAX_LEVEL = 2;

    private float _blockChance = 0.2f;
    private float _cooldown = 10;
    private float _timeToActivation;
    private bool _isActive;

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

    public override void Activate()
    {
        _isActive = true;
    }

    public override void Deactivate()
    {
        if (!_isActive) return;
        _isActive = false;
        _timeToActivation = _cooldown;
    }

    public override void Update()
    {
        TryActivate();
    }

    public ShieldChip(int currentLevel) : base(currentLevel)
    {
        maxLevel = MAX_LEVEL;
    }

    public ShieldChip()
    {
        maxLevel = MAX_LEVEL;
    }

    private void TryActivate()
    {
        if (_isActive) return;
        if (_timeToActivation <= 0) Activate();
        else _timeToActivation -= Time.deltaTime;
    }

    public bool TryBlock()
    {
        return _isActive && Randomiser.Succeed(_blockChance);
    }
}