using Creatures.AI;
using System.Collections;
using UnityEngine;

namespace Creatures.Enemy
{
    public class TypeBEnemyController : EnemyController
    {
        private const float ATTACK_1_VELOCITY = 100f;
        private const float ATTACK_1_DRUG = 5f;
        private const float ATTACK_1_TIME = 1f;

        public void Attack1()
            => StartCoroutine(Attack1Routine());

        private IEnumerator Attack1Routine()
        {
            GetComponent<AINavigation>().StopNavigation();
            float linearDrag = _rigidbody.drag;
            _rigidbody.drag = ATTACK_1_DRUG;
            _rigidbody.velocity = new Vector2(ATTACK_1_VELOCITY * transform.localScale.x * (_invertScale ? -1f : 1f), 0f);
            yield return new WaitForSeconds(ATTACK_1_TIME);
            _rigidbody.drag = linearDrag;
            _rigidbody.velocity = Vector2.zero;
            yield return new WaitForSeconds(1f);
            GetComponent<AINavigation>().StartNavigation();
        }
    }
}
