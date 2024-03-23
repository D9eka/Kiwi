using UnityEngine;

[CreateAssetMenu]
public class ChipSO : ScriptableObject
{
    [SerializeField] private Sprite _image;
    [SerializeField] private string _name;
    [SerializeField] private ChipType _chipType;
    public ChipType ChipType => _chipType;
}