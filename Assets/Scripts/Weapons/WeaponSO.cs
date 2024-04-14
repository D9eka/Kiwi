using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

[CreateAssetMenu]
public class WeaponSO : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private GameObject _weaponPrefab;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private bool _isConsumable;

    public Sprite Sprite => _sprite;
    public string Name => _name;
    public string Description => _description;
    public GameObject WeaponPrefab => _weaponPrefab;
    public bool IsConsumable => _isConsumable;
}