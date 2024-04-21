using System;
using System.Collections;
using System.Collections.Generic;
using Sections;
using UnityEngine;

public class CurrentSectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int SectionNumber { get; private set; }

    [SerializeField] private bool _isCompleted;
    [SerializeField] private bool _isBattleOver;
    [SerializeField] private bool _hasAdditionalCompleteConditions;
    [SerializeField] private bool _isOxygenWasting;
    public bool IsCompleted => _isCompleted;
    public bool IsOxygenWasting => _isOxygenWasting;
    public static CurrentSectionManager Instance { get; private set; }
    public event EventHandler OnSectionComplete;
    public event EventHandler OnBattleOver;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SectionNumber = SectionManager.Instance.CurrentSectionIndex;
        if (_isBattleOver) EndBattle();
        if (_isCompleted) Complete();
    }

    [ContextMenu(nameof(Complete))]
    public void Complete()
    {
        _isCompleted = true;
        OnSectionComplete?.Invoke(this, EventArgs.Empty);
    }

    public void EndBattle()
    {
        _isBattleOver = true;
        OnBattleOver?.Invoke(this, EventArgs.Empty);
        if (!_hasAdditionalCompleteConditions) Complete();
    }
}