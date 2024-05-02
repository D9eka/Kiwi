using Components.Health;
using Creatures.Player;
using UnityEngine;

namespace Creatures.Enemy
{
    public class KamikazeEnemyController : EnemyController
    {
        protected override void Awake()
        {
            base.Awake();
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }

        protected override void Vision_OnPlayerEnterTrigger(object sender, System.EventArgs e)
        {
            Activate();
        }

        private void Activate()
        {
            _activeAttack = _attacks[0];
            _animator.SetTrigger(_attacks[0].Attack());
        }

        protected override void Update()
        {
            return;
        }

        public override void OnAttack()
        {
            foreach (HealthComponent health in _activeAttack.OnAttack())
            {
                if (health == GetComponent<HealthComponent>())
                    continue;

                if (health == PlayerController.Instance.GetComponent<HealthComponent>())
                {
                    if (!PlayerController.Instance.Active)
                        continue;
                    MyGameManager.AddEarnedDamage(_visual.GetComponent<SpriteRenderer>().sprite, _activeAttack.Damage);
                }
                health.ModifyHealth(-_activeAttack.Damage);
            }
            Destroy(gameObject);
        }
    }
}
