﻿using Creatures;
using Creatures.AI;
using Creatures.Enemy;
using Creatures.Player;
using Pathfinding;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Creatures.AI
{
    [RequireComponent(typeof(Seeker))]
    public class AStartAINavigation : AINavigation
    {
        [SerializeField] private GameObject _targetPrefab;
        [SerializeField] private GameObject _target;
        [Header("Pathfinder")]
        [SerializeField] private bool _isFlying;
        [SerializeField] private float _pathUpdateSeconds = 0.5f;

        private Seeker _seeker;
        private Path _path;
        private int _currentWaypoint = 0;
        private float _threshold;

        private GameObject _spawnedTargetBackingField;
        private GameObject _spawnedTarget
        {
            get => _spawnedTargetBackingField;
            set
            {
                if (_spawnedTargetBackingField != null)
                    Destroy(_spawnedTargetBackingField);

                _spawnedTargetBackingField = value;
            }
        }

        private Creature _creature;

        private int _patrollingPointIndex;

        private bool _followEnabled;

        public EventHandler<bool> OnChasing;

        private void Awake()
        {
            _threshold = _patrollingThreshold;

            _seeker = GetComponent<Seeker>();
            _creature = GetComponent<Creature>();

            InvokeRepeating(nameof(UpdatePath), 0, _pathUpdateSeconds);
        }

        public override void StartNavigation()
        {
            _followEnabled = true;
        }

        public override void StopNavigation()
        {
            _followEnabled = false;
        }

        public GameObject GetTarget()
        {
            return _target;
        }

        public override void SetTarget(Vector2 target)
        {
            if (_spawnedTarget != null && (Vector2)_spawnedTarget.transform.position == target)
                return;
            _spawnedTarget = Instantiate(_targetPrefab, target, Quaternion.identity);
            SetTarget(gameObject);
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        private void Update()
        {
            if (!_followEnabled || _path == null || _currentWaypoint >= _path.vectorPath.Count)
            {
                _creature.SetDirection(Vector2.zero);
                return;
            }

            Vector2 direction = (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
            if (_isFlying)
                _creature.SetDirection(direction);
            else
                _creature.SetDirection(new Vector2(direction.x, 0f));

            if (Vector2.Distance(transform.position, _path.vectorPath[_currentWaypoint]) < _threshold)
            {
                _currentWaypoint++;
            }
        }

        private void UpdatePath()
        {
            if (_target != null && _seeker.IsDone())
            {
                _seeker.StartPath(transform.position, _target.transform.position, OnPathComplete);
                _currentWaypoint = 0;
            }
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                _path = p;
                _currentWaypoint = 0;
            }
        }

        public override void StartPatrol()
        {
            _target = null;
            _spawnedTarget = null;

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
            _target = _points[_patrollingPointIndex];
            while (enabled)
            {
                if (Vector2.Distance(transform.position, _points[_patrollingPointIndex].transform.position) <= _threshold)
                {
                    _patrollingPointIndex = (int)Mathf.Repeat(_patrollingPointIndex + 1, _points.Length);
                    _target = _points[_patrollingPointIndex];
                }
                yield return null;
            }
        }

        public float GetDistanceToNearestPoint()
        {
            return _points.Select(gameObject => Vector2.Distance(gameObject.transform.position, _creature.transform.position))
                          .Min();
        }

        public override Vector2 GetChasingPoint()
        {
            if (_points.Length == 0 || _isFlying)
            {
                OnChasing?.Invoke(this, true);
                return new Vector2(PlayerController.Instance.transform.position.x - _chasingOffset * transform.localScale.x,
                                   PlayerController.Instance.transform.position.y);
            }

            if (PlayerController.Instance.transform.position.x < _points[0].transform.position.x)
            {
                OnChasing?.Invoke(this, false);
                return _points[0].transform.position;
            }

            if (PlayerController.Instance.transform.position.x > _points[^1].transform.position.x)
            {
                OnChasing?.Invoke(this, false);
                return _points[^1].transform.position;
            }

            OnChasing?.Invoke(this, true);
            return new Vector2(PlayerController.Instance.transform.position.x - _chasingOffset * transform.localScale.x,
                               transform.position.y);
        }
    }
}