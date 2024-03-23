using System;
using System.Collections.Generic;
using UnityEngine;

public class ChipManager : MonoBehaviour
{
    public static ChipManager Instance { get; private set; }
    private List<Chip> _obtainedChips = new();

    private List<UpdatingChip> _obtainedUpdatingChips = new();

    //Тестовый вариант
    private ShieldChip _shieldChip;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        foreach (var chip in _obtainedUpdatingChips)
        {
            chip.Update();
        }
    }

    public void ObtainChip(Chip chip)
    {
        _obtainedChips.Add(chip);
        if (chip.GetType().IsSubclassOf(typeof(UpdatingChip)))
        {
            _obtainedUpdatingChips.Add((UpdatingChip)chip);
            //Тестовый вариант
            if (chip.GetType() == typeof(ShieldChip))
            {
                _shieldChip = (ShieldChip)chip;
            }
        }

        chip.Activate();
    }

    public void RemoveChip(Chip chip)
    {
        _obtainedChips.Remove(chip);
        if (chip.GetType().IsSubclassOf(typeof(UpdatingChip)))
        {
            _obtainedUpdatingChips.Remove((UpdatingChip)chip);
            //Тестовый вариант
            if (chip.GetType() == typeof(UpdatingChip))
                _shieldChip = null;
        }

        chip.Deactivate();
    }

    //Тестовый вариант
    // Задумка в том, чтобы в коде, где игрок получает урон, пытаться вызвать этот метод
    public bool TryUseShieldChip()
    {
        if (_shieldChip == null || !_shieldChip.TryBlock())
        {
            return false;
        }

        _shieldChip.Deactivate();
        return true;
    }
}