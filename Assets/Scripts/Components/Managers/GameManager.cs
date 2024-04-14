using System;
using Sections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool _wasKeyCardGenerated;
    private float _oxygenSecondConsumption = 1;
    public int EssenceCount { get; private set; }
    public int KeyCardCount { get; private set; }
    public float Oxygen { get; private set; }
    public float MaxOxygen { get; private set; } = 300;

    public int Gravity { get; private set; } = 1;

    public event EventHandler OnEssenceCountChanged;
    public event EventHandler OnGravityInverted;

    public delegate void OxygenCountEventHandler(float oxygen, float maxOxygen);

    public event OxygenCountEventHandler OnOxygenValueChanged;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Oxygen = MaxOxygen;
    }

    private void FixedUpdate()
    {
        ChangeOxygenValue(-_oxygenSecondConsumption * Time.deltaTime);
    }


    public void GetEssence(int count)
    {
        EssenceCount += count;
        OnEssenceCountChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool TrySpendEssence(int count)
    {
        if (!CanSpendEssence(count)) return false;
        EssenceCount -= count;
        OnEssenceCountChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public bool CanSpendEssence(int count)
    {
        return count <= EssenceCount;
    }

    public void GetKeyCard(int count)
    {
        KeyCardCount += count;
        _wasKeyCardGenerated = true;
    }

    public bool TryUseKeyCard(int count)
    {
        if (count > EssenceCount) return false;
        KeyCardCount -= count;
        return true;
    }

    public void InvertGravity()
    {
        Gravity *= -1;
        OnGravityInverted?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeOxygenValue(float value)
    {
        Oxygen += value;
        Oxygen = Math.Min(MaxOxygen, Oxygen);
        OnOxygenValueChanged?.Invoke(Oxygen, MaxOxygen);
    }

    public void ChangeOxygenPercent(float percent)
    {
        ChangeOxygenValue(percent * MaxOxygen);
    }
}