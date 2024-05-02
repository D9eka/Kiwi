using UnityEngine;

namespace Creatures.Enemy.States
{
    public class TypeAJumpBackState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.transform.parent.GetComponent<TypeAEnemyController>().JumpBack();
        }
    }
}
