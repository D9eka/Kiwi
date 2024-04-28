using Creatures.Player;
using System;
using System.Collections;
using UnityEngine;

namespace Creatures.AI
{
    public class SimpleAINavigation : AINavigation
    {
        private Vector2 _target;
        private float _threshold;
        private bool _followEnabled;
        private int _patrollingPointIndex;

        private Creature _creature;

        public EventHandler<bool> OnChasing;

        private void Awake()
        {
            _threshold = _patrollingThreshold;
            _creature = GetComponent<Creature>();
        }

        public override void StartNavigation()
        {
            _followEnabled = true;
        }

        public override void StopNavigation()
        {
            _followEnabled = false;
        }

        public override void SetTarget(Vector2 target)
        {
            _target = target;
        }

        private void Update()
        {
            if (!_active && !_followEnabled || Vector2.Distance(transform.position, _target) < _threshold)
            {
                _creature.SetDirection(Vector2.zero);
                return;
            }

            Vector2 direction = (_target - (Vector2)transform.position).normalized;
            _creature.SetDirection(new Vector2(direction.x, 0f));
        }

        public override void StartPatrol()
        {
            _threshold = _patrollingThreshold;
            StartCoroutine(DoPatrol());
        }

        public override void StopPatrol()
        {
            StopCoroutine(DoPatrol());
            _threshold = _setTargetThreshold;
        }

        private IEnumerator DoPatrol()
        {
            if (!_active)
                yield return new WaitUntil(() => _active == true);

            _target = _points[_patrollingPointIndex].transform.position;
            while (enabled)
            {
                if (Vector2.Distance(transform.position, _points[_patrollingPointIndex].transform.position) <= _threshold)
                {
                    _patrollingPointIndex = (int)Mathf.Repeat(_patrollingPointIndex + 1, _points.Length);
                    _target = _points[_patrollingPointIndex].transform.position;
                }
                yield return null;
            }
        }

        public override Vector2 GetChasingPoint()
        {
            OnChasing?.Invoke(this, true);
            return new Vector2(PlayerController.Instance.transform.position.x - _chasingOffset * transform.localScale.x,
                               transform.position.y);
        }
    }
}