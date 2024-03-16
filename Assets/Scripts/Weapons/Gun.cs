using UnityEngine;
using Extensions;

namespace Weapons
{
    public class Gun : Weapon
    {
        [Header("Bullet")]
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _shotPoint;

        [SerializeField] private int _ammoCapacity;

        private int ammoCount;

        private void Start()
        {
            ammoCount = _ammoCapacity;
        }

        public override void Attack()
        {
            if (timeBetweenAttacks < _attackDelay)
                return;

            if (ammoCount <= 0)
                return;

            GameObject bullet = Instantiate(_bullet, _shotPoint.position, transform.rotation);
            bullet.GetComponent<Bullet>().Initialize(_damage);
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

            if (transform.parent.parent.localScale.x > 0)
                transform.localScale = new Vector2(1f, 1f);
            else
                transform.localScale = new Vector2(-1f, -1f);
        }
    }
}
