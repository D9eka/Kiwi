using Creatures.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Creatures.AI
{
    public abstract class AINavigation : MonoBehaviour
    {
        [SerializeField] protected float _patrollingThreshold = 0.25f;
        [SerializeField] protected float _setTargetThreshold = 1.5f;

        [Header("Patrolling")]
        [SerializeField] protected GameObject[] _points;

        [Header("Chasing")]
        [SerializeField] protected float _chasingOffset = 1f;

        public abstract void SetTarget(Vector2 target);

        public abstract void StartNavigation();

        public abstract void StopNavigation();

        public abstract void StartPatrol();

        public abstract void StopPatrol();

        public abstract Vector2 GetChasingPoint();
    }
}
