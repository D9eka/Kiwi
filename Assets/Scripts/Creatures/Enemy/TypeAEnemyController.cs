using Creatures.AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Creatures.Enemy
{
    public class TypeAEnemyController : EnemyController
    {
        private bool _canReachPlayer = true;

        private const float JUMP_BACK_DISTANCE = 3f;
        private const float JUMP_BACK_TIME = 1f;

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

        public void JumpBack()
            => StartCoroutine(JumpBackRoutine());

        private IEnumerator JumpBackRoutine()
        {
            Vector2 direction = new Vector2(transform.localScale.x * (_invertScale ? 1f : -1f), 0);
            Vector3 initialPosition = transform.position;

            Vector3 endPosition = new Vector3(initialPosition.x + (JUMP_BACK_DISTANCE * direction.x), initialPosition.y, initialPosition.z);
            float startTime = Time.time;

            while (transform.position != endPosition)
            {
                float elapsedTime = (Time.time - startTime) / JUMP_BACK_TIME;
                transform.position = Vector3.Lerp(initialPosition, endPosition, elapsedTime);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
