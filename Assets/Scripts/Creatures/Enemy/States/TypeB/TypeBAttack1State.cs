using UnityEngine;

namespace Creatures.Enemy.States.TypeB
{
    public class TypeBAttack1State : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.transform.parent.GetComponent<TypeBEnemyController>().Attack1();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            EnemyController enemy = animator.GetComponentInParent<EnemyController>();
            enemy.OnAttack();
            enemy.StartInitialState();
        }
    }
}
