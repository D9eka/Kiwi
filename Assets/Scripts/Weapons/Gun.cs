using UnityEngine;
using Extensions;
using Creatures.Player;

namespace Weapons
{
    public class Gun : Weapon
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _shotPoint;

        private int ammoCount;

        private void Start()
        {
            ammoCount = _data.AmmoCapacity;
        }

        public override void Attack()
        {
            base.Attack();

            if (_timeBetweenAttacks < _data.AttackDelay || ammoCount <= 0)
                return;

            GameObject bullet = Instantiate(_bullet, _shotPoint.position, transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(_currentDamage, _data.BulletSpeed, _data.BulletTTLSeconds);
            _timeBetweenAttacks = 0;
            ammoCount--;
            Debug.Log($"{ammoCount} / {_data.AmmoCapacity}");
        }

        public void Reload()
        {
            ammoCount = _data.AmmoCapacity;
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