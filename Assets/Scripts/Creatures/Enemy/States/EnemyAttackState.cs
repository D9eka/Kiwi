using UnityEngine;

namespace Creatures.Enemy.States
{
    public class EnemyAttackState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            EnemyController enemy = animator.GetComponentInParent<EnemyController>();
            enemy.OnAttack();
            enemy.StartInitialState();
        }
    }
}
