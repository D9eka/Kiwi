using Sections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChipManager : MonoBehaviour
{
    [SerializeField] private List<ChipSO> _possibleChips = new();
    [SerializeField] private ChipSO _brokenChipSO;

    private List<Chip> _obtainedChips = new();
    private List<UpdatingChip> _obtainedUpdatingChips = new();
    private ShieldChip _shieldChip;
    private RevivalChip _revivalChip;
    private VampirismChip _vampirismChip;

    public List<ChipSO> PossibleChips
    {
        get
        {
            List<ChipSO> obtainedChipSO = _obtainedChips.Select(chip => chip.ChipSO).ToList();
            return _possibleChips.Where(chip => chip == _brokenChipSO || !obtainedChipSO.Contains(chip)).ToList();
        }
    }
    public List<Chip> ObtainedChips => _obtainedChips;

    public event EventHandler OnStateChange;
    public static ChipManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (SectionManager.Instance != null)
            SectionManager.Instance.OnStartLoadingSection += SectionManager_OnStartLoadingSection;
        if (SectionTutorial.Instance != null)
            SectionTutorial.Instance.OnStartLoadingSection += SectionManager_OnStartLoadingSection;
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        yield return new WaitForFixedUpdate();
        if (ChipManagerData.ObtainedChips != null)
        {
            for (int i = 0; i < ChipManagerData.ObtainedChips.Count; i++)
            {
                ObtainChip(ChipManagerData.ObtainedChips[i], ChipManagerData.ObtainedChipLevels[i]);
            }
        }
    }

    private void SectionManager_OnStartLoadingSection(object sender, EventArgs e)
    {
        ChipManagerData.ObtainedChips = _obtainedChips.Select(chip => chip.ChipSO).ToList();
        ChipManagerData.ObtainedChipLevels = _obtainedChips.Select(chip => chip.CurrentLevel).ToList();
        foreach (Chip chip in _obtainedChips)
        {
            if (chip is PassiveChip passiveChip)
                passiveChip.Deactivate();
        }
    }

    private void Update()
    {
        foreach (var chip in _obtainedUpdatingChips)
        {
            chip.Update();
        }
    }

    public void ObtainChip(ChipSO chipSO, int level = 1)
    {
        var chip = ChipCreator.Create(chipSO);
        while (chip.CurrentLevel != level)
            chip.TryUpgrade();
        _obtainedChips.Add(chip);
        if (chip is UpdatingChip updatingChip)
        {
            _obtainedUpdatingChips.Add(updatingChip);
        }

        TrySetChip(chip);
        if (chip is PassiveChip passiveChip)
            passiveChip.Activate();
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

    public bool TryUseRevivalChip(out float healthPercent)
    {
        if (_revivalChip == null)
        {
            healthPercent = 0;
            return false;
        }

        healthPercent = _revivalChip.Revive();
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

public static class ChipManagerData
{
    public static List<ChipSO> ObtainedChips;
    public static List<int> ObtainedChipLevels;

    public static void Clear()
    {
        ObtainedChips = null;
        ObtainedChipLevels = null;
    }
}