using UnityEngine;
using Extensions;
using Creatures.Player;

namespace Weapons
{
    public class Gun : Weapon
    {
        [Header("Bullet")] [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _shotPoint;

        [SerializeField] private float _speed;
        [SerializeField] private float _ttl;
        [SerializeField] private int _ammoCapacity;

        private int ammoCount;
        public int AmmoCapacity => _ammoCapacity;
        public int AmmoCount => ammoCount;

        private void Start()
        {
            ammoCount = _ammoCapacity;
        }

        public override void Attack()
        {
            base.Attack();

            if (_timeBetweenAttacks < _attackDelay || ammoCount <= 0)
                return;

            GameObject bullet = Instantiate(_bullet, _shotPoint.position, transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(_currentDamage, _speed, _ttl);
            _timeBetweenAttacks = 0;
            ammoCount--;
            Debug.Log($"{ammoCount} / {_ammoCapacity}");
        }

        public void Reload()
        {
            ammoCount = _ammoCapacity;
            Debug.Log("Reload");
        }

        public void UpdateSpriteDirection()
        {
            float angle = transform.GetAngleToMouse();
            transform.eulerAngles = new Vector3(0, 0, angle);

            if (PlayerController.Instance.transform.localScale.x > 0)
                transform.localScale = new Vector2(1f, 1f * GameManager.Instance.Gravity);
            else
                transform.localScale = new Vector2(-1f, -1f * GameManager.Instance.Gravity);
        }
    }
}