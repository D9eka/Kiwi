using Creatures.Player;
using Extensions;
using UnityEngine;

namespace Weapons
{
    public class Gun : Weapon
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _shotPoint;

        private int ammoCount;

        private void Awake()
        {

            ammoCount = _data.AmmoCapacity;
            Label = $"{ammoCount} / {_data.AmmoCapacity}";
        }

        public override void Attack()
        {
            if (_timeBetweenAttacks < _data.AttackDelay || ammoCount <= 0)
                return;

            base.Attack();

            GameObject bullet = Instantiate(_bullet, _shotPoint.position, transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(_currentDamage, _data.BulletSpeed, _data.BulletTTLSeconds, _data.DamageTypeUI == "Энергетический");
            _timeBetweenAttacks = 0;
            ammoCount--;
            Label = $"{ammoCount} / {_data.AmmoCapacity}";
        }

        public void Reload()
        {
            if (ammoCount == _data.AmmoCapacity)
                return;
            ammoCount = _data.AmmoCapacity;
            Label = $"{ammoCount} / {_data.AmmoCapacity}";
        }

        public void UpdateSpriteDirection()
        {
            float angle = transform.GetAngleToMouse();
            transform.eulerAngles = new Vector3(0, 0, angle);

            if (PlayerController.Instance.transform.localScale.x > 0)
                transform.localScale = new Vector2(1f, 1f * MyGameManager.Gravity);
            else
                transform.localScale = new Vector2(-1f, -1f * MyGameManager.Gravity);
        }
    }
}