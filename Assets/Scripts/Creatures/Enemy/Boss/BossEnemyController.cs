using Components.Health;
using Creatures.AI;
using System.Linq;
using UnityEngine;

namespace Creatures.Enemy
{
    public class BossEnemyController : EnemyController
    {
        [SerializeField] private AttackComponent[] _speedUpAttacks;

        private HealthComponent _health;

        private bool _speedUp;
        private const string SPEED_UP_KEY = "speed-up";

        protected override void Awake()
        {
            base.Awake();
            _health = GetComponent<HealthComponent>();
        }

        protected override void Start()
        {
            base.Start();

            _health.OnValueChange += Health_OnValueChange;

            GetComponent<AINavigation>().Initialize();
        }

        protected override void ChooseAttack()
        {
            AttackComponent[] availableAttacks = _speedUp ? 
                                                 _speedUpAttacks.Where(attack => attack.CanAttack).ToArray() : 
                                                 _attacks.Where(attack => attack.CanAttack).ToArray();
            if (availableAttacks.Length > 0)
            {
                SetState(EnemyState.Attack);
                if (availableAttacks.Length == 1)
                {
                    _activeAttack = availableAttacks[0];
                    _animator.SetTrigger(availableAttacks[0].Attack());
                }
                if (availableAttacks.Length > 1)
                {
                    int randomAttack = UnityEngine.Random.Range(0, availableAttacks.Length - 1);
                    _activeAttack = availableAttacks[randomAttack];
                    _animator.SetTrigger(availableAttacks[randomAttack].Attack());
                }
            }
        }

        private void Health_OnValueChange(object sender, HealthComponent.OnValueChangeEventArgs e)
        {
            if (e.value < 58)
            {
                _speedUp = true;
                _animator.SetBool(SPEED_UP_KEY, true);
            }
        }
    }
}