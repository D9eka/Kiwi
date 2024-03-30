using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChipSO : ScriptableObject
{
    [SerializeField] private Sprite _image;
    [SerializeField] private string _name;
    [SerializeField] private List<string> _descriptions;
    [SerializeField] private ChipType _chipType;
    public Sprite Image => _image;
    public string Name => _name;
    public List<string> Description => _descriptions;

    public ChipType ChipType => _chipType;
}