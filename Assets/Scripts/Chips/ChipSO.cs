﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChipSO : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private List<string> _descriptions = new();

    [SerializeField] private ChipType _chipType;

    public Sprite Sprite => _sprite;
    public string Name => _name;
    public string Description => _descriptions[ChipCreator.Create(this).CurrentLevel - 1];
    public List<string> Descriptions => _descriptions;
    public ChipType ChipType => _chipType;
}