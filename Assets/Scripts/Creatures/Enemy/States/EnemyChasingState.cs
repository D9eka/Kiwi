using Creatures.AI;
using Creatures.Player;
using UnityEngine;

namespace Creatures.Enemy.States
{
    public class EnemyChasingState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AINavigation navigation = animator.GetComponentInParent<AINavigation>();
            navigation.StartNavigation();
            navigation.SetTarget(PlayerController.Instance.transform);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AINavigation navigation = animator.GetComponentInParent<AINavigation>();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponentInParent<AINavigation>().StopNavigation();
        }
    }
}
