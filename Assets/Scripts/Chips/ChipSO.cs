using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class ChipSO : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private List<string> _descriptions = new ();

    [SerializeField] private ChipType _chipType;

    // [SerializeField] private Chip _chip;
    public Sprite Sprite => _sprite;
    public string Name => _name;
    public string Description => _descriptions[ChipCreator.Create(this).CurrentLevel - 1];
    public ChipType ChipType => _chipType;
    // public Chip Chip => _chip;
}