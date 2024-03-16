using Components.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Weapons
{
    public class Melee : Weapon
    {
        [SerializeField] private CircleCollider2D _attackRange;

        public override void Attack()
        {
            if (timeBetweenAttacks < _attackDelay)
                return;

            _animator.SetTrigger(ATTACK_KEY);
        }

        public void OnAttack()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackRange.transform.position, _attackRange.radius);
            foreach (Collider2D collider in colliders)
            {
                if(collider.gameObject.TryGetComponent(out HealthComponent health))
                {
                    health.ModifyHealth(-_damage);
                }
            }
        }    
    }
}
