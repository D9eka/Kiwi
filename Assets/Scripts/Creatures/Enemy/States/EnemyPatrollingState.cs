using Creatures.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Creatures.Enemy.States
{
    public class EnemyPatrollingState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AINavigation navigation = animator.GetComponentInParent<AINavigation>();
            navigation.StartNavigation();
            navigation.StartPatrol();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AINavigation navigation = animator.GetComponentInParent<AINavigation>();
            navigation.StopNavigation();
            navigation.StopPatrol();
        }
    }
}
