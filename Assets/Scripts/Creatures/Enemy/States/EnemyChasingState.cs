using Creatures.AI;
using Creatures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Creatures.Enemy.States
{
    public class EnemyChasingState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AINavigation navigation = animator.GetComponentInParent<AINavigation>();
            navigation.StartNavigation();
            navigation.SetTarget(navigation.GetChasingPoint());
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AINavigation navigation = animator.GetComponentInParent<AINavigation>();
            navigation.SetTarget(navigation.GetChasingPoint());
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponentInParent<AINavigation>().StopNavigation();
        }
    }
}
