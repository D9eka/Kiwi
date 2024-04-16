using System;
using System.Collections.Generic;
using UnityEngine;

public class ChipManager : MonoBehaviour
{
    [SerializeField] private List<ChipSO> _possibleChips = new();

    public static ChipManager Instance { get; private set; }
    private List<Chip> _obtainedChips = new();

    private List<UpdatingChip> _obtainedUpdatingChips = new();

    //Тестовый вариант
    private ShieldChip _shieldChip;
    private RevivalChip _revivalChip;
    private VampirismChip _vampirismChip;
    [SerializeField] private ChipSO _brokenChipSO;
    public List<ChipSO> PossibleChips => _possibleChips;
    public List<Chip> ObtainedChips => _obtainedChips;
    public event EventHandler OnStateChange;

    private void Awake()
    {
        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        foreach (var chip in _obtainedUpdatingChips)
        {
            chip.Update();
        }
    }

    public void ObtainChip(ChipSO chipSO)
    {
        // var chip = chipSO.Chip;
        var chip = ChipCreator.Create(chipSO);
        _obtainedChips.Add(chip);
        if (chip is not BrokenChip) _possibleChips.Remove(chipSO);
        if (chip is UpdatingChip updatingChip)
        {
            _obtainedUpdatingChips.Add(updatingChip);
        }

        TrySetChip(chip);
        if (chip is PassiveChip passiveChip) passiveChip.Activate();
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    public void ObtainBrokenChip()
    {
        var chip = ChipCreator.Create(_brokenChipSO);
        _obtainedChips.Add(chip);
        if (chip is UpdatingChip updatingChip)
        {
            _obtainedUpdatingChips.Add(updatingChip);
        }

        TrySetChip(chip);
        if (chip is PassiveChip passiveChip) passiveChip.Activate();
    }

    public void RemoveChip(Chip chip)
    {
        _obtainedChips.Remove(chip);
        if (chip is UpdatingChip updatingChip)
        {
            _obtainedUpdatingChips.Remove(updatingChip);
        }

        UnsetChip(chip);
        if (chip is PassiveChip passiveChip) passiveChip.Deactivate();
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    public bool TryUseShieldChip()
    {
        return _shieldChip != null && _shieldChip.TryBlock();
    }

    public bool TryUseRevivalChip()
    {
        if (_revivalChip == null)
        {
            return false;
        }

        _revivalChip.Revive();
        return true;
    }

    public void TryUseVampirismChip()
    {
        _vampirismChip?.TryRestoreHealth();
    }

    private void TrySetChip(Chip chip)
    {
        switch (chip)
        {
            case ShieldChip shieldChip:
                _shieldChip = shieldChip;
                break;
            case RevivalChip revivalChip:
                _revivalChip = revivalChip;
                break;
            case VampirismChip vampirismChip:
                _vampirismChip = vampirismChip;
                break;
        }
    }

    private void UnsetChip(Chip chip)
    {
        switch (chip)
        {
            case ShieldChip:
                _shieldChip = null;
                break;
            case RevivalChip:
                _revivalChip = null;
                break;
            case VampirismChip:
                _vampirismChip = null;
                break;
        }
    }
}