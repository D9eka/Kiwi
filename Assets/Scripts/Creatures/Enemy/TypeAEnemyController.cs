using Creatures.AI;
using System.Collections;
using UnityEngine;

namespace Creatures.Enemy
{
    public class TypeAEnemyController : EnemyController
    {
        private const float JUMP_BACK_FORCE = 30f;
        private const float JUMP_BACK_DRUG = 5f;
        private const float JUMP_BACK_TIME = 1f;

        public void JumpBack()
            => StartCoroutine(JumpBackRoutine());

        private IEnumerator JumpBackRoutine()
        {
            GetComponent<AINavigation>().StopNavigation();
            float linearDrag = _rigidbody.drag;
            _rigidbody.drag = JUMP_BACK_DRUG;
            _rigidbody.velocity = new Vector2(JUMP_BACK_FORCE * transform.localScale.x * (_invertScale ? 1f : -1f), 0f);
            yield return new WaitForSeconds(JUMP_BACK_TIME);
            _rigidbody.drag = linearDrag;
            _rigidbody.velocity = Vector2.zero;
            yield return new WaitForSeconds(1f);
            GetComponent<AINavigation>().StartNavigation();
        }
    }
}
