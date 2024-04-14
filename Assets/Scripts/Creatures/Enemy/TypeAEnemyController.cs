using Creatures.AI;
using System.Collections.Generic;
using System.Linq;

namespace Creatures.Enemy
{
    public class TypeAEnemyController : EnemyController
    {
        private bool _canReachPlayer = true;

        protected override void Start()
        {
            base.Start();

            GetComponent<SimpleAINavigation>().OnChasing += AINavigation_OnChasing;
        }

        private void AINavigation_OnChasing(object sender, bool canReachPlayer)
        {
            _canReachPlayer = canReachPlayer;
        }

        protected override void ChooseAttack()
        {
            List<AttackComponent> availableAttacks = _attacks.Where(attack => attack.CanAttack).ToList();
            if (_canReachPlayer && availableAttacks.Contains(_attacks[1]))
                availableAttacks.Remove(_attacks[1]);

            if (availableAttacks.Count > 0)
            {
                SetState(EnemyState.Attack);
                if (availableAttacks.Count == 1)
                {
                    _activeAttack = availableAttacks[0];
                    _animator.SetTrigger(availableAttacks[0].Attack());
                }
                if (availableAttacks.Count > 1)
                {
                    int randomAttack = UnityEngine.Random.Range(0, availableAttacks.Count - 1);
                    _activeAttack = availableAttacks[randomAttack];
                    _animator.SetTrigger(availableAttacks[randomAttack].Attack());
                }
            }
        }
    }
}
