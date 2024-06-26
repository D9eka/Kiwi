using Sections;
using UnityEngine;
using static SoundManager;

[CreateAssetMenu]
public class SectionTypeSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private SectionType _sectionType;
    //[SerializeField] private Sprite _image;
    [SerializeField] private BGMType _backgroundType;
    [Space]
    [SerializeField] private int _wavesCount = 3;
    [SerializeField] private int _spawnPointBonus;
    [Space]
    [SerializeField] private bool _isOxygenWasting = true;

    public SectionType SectionType => _sectionType;
    public string Name => _name;
    //public Sprite Image => _image;
    public BGMType BackgroundType => _backgroundType;

    public int WavesCount => _wavesCount;
    public int SpawnPointBonus => _spawnPointBonus;

    public bool IsOxygenWasting => _isOxygenWasting;
}