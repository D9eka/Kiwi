using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu]
    public class WeaponSO : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private string _animKey;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private AudioClip _sound;
        [SerializeField] private int _price;
        [Space]
        [SerializeField] private float _attackDelay;
        [SerializeField] private string _damageTypeUI;
        [SerializeField] private WeaponDamageType _damageType;
        [SerializeField] private float _damage;
        [SerializeField] private float _minDamage;
        [SerializeField] private float _maxDamage;
        [Space]
        [SerializeField] private WeaponType _type;
        [SerializeField] private int _ammoCapacity;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletTTLSeconds;

        [SerializeField] private GameObject _thrownTrapPrefab;
        [SerializeField] private AudioClip _thrownTrapSound;
        [SerializeField] private int _maxAmount;
        [SerializeField] private float _thrownDelay;
        [SerializeField] private TrapDestroyType _destroyType;
        [SerializeField] private float _TTLSeconds;

        public enum WeaponDamageType
        {
            Static,
            Random
        }

        public enum WeaponType
        {
            Melee,
            Gun,
            Trap
        }

        public enum TrapDestroyType
        {
            AfterAttack,
            TimeLimit
        }

        public string Name => _name;
        public GameObject Prefab => _prefab;
        public string AnimKey => _animKey;
        public string Description => _description;
        public Sprite Icon => _icon;
        public AudioClip Sound => _sound;
        public int Price => StatsModifier.GetModifiedPrice(_price);

        public float AttackDelay => _attackDelay;
        public string DamageTypeUI => _damageTypeUI;
        public WeaponDamageType DamageType => _damageType;
        public float Damage => _damage;
        public float MinDamage => _minDamage;
        public float MaxDamage => _maxDamage;

        public WeaponType Type => _type;
        public int AmmoCapacity => _ammoCapacity;
        public float BulletSpeed => _bulletSpeed;
        public float BulletTTLSeconds => _bulletTTLSeconds;

        public GameObject ThrownTrapPrefab => _thrownTrapPrefab;
        public AudioClip ThrownTrapSound => _thrownTrapSound;
        public int MaxAmount => _maxAmount;
        public float ThrownDelay => _thrownDelay;
        public TrapDestroyType DestroyType => _destroyType;
        public float TTLSeconds => _TTLSeconds;


        public float AttackSpeed => 1 / _attackDelay;
        public string DamageInfo
        {
            get
            {
                return _damageType switch
                {
                    WeaponDamageType.Static => _damage.ToString(),
                    WeaponDamageType.Random => _minDamage + "-" + _maxDamage,
                    _ => "-"
                };
            }
        }

        public string Range => _type == WeaponType.Gun ? ((int)BulletTTLSeconds).ToString() : "-";
        public string Ammo => _type switch
        {
            WeaponType.Melee => "-",
            WeaponType.Gun => _ammoCapacity.ToString(),
            WeaponType.Trap => _maxAmount.ToString(),
            _ => "-"
        };
    }
}