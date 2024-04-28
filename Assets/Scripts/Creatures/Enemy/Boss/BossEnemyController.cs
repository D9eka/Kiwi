using Components.Health;
using System;
using System.Collections;
using UnityEngine;

namespace Creatures.Enemy
{
    public class BossEnemyController : EnemyController
    {
        [SerializeField] private Transform _attack1Start;
        [SerializeField] private Transform _attack1Center;
        [SerializeField] private Transform _attack1End;
        [SerializeField] private float _attack1MaxHeight;

        private HealthComponent _health;

        private const string SPEED_UP_KEY = "speed-up";

        protected override void Awake()
        {
            base.Awake();
            _health = GetComponent<HealthComponent>();
        }

        protected override void Start()
        {
            base.Start();

            _health.OnValueChange += Health_OnValueChange;
        }

        private void Health_OnValueChange(object sender, HealthComponent.OnValueChangeEventArgs e)
        {
            _animator.SetBool(SPEED_UP_KEY, e.value < 58);
        }

        public void Attack1()
            => StartCoroutine(Attack1Routine());
        
        private IEnumerator Attack1Routine()
        {
            _activeAttack = _attacks[0];
            float directionX = _attack1Center.position.x - transform.position.x > 0 ? 1 : -1;
            yield return MoveRoutine(new Vector2(directionX, 1), _attack1Center.position.x - transform.position.x, _attack1MaxHeight, 1f);
            OnAttack();

            _activeAttack = _attacks[0];
            yield return new WaitForSeconds(0.166f);
            directionX = _attack1Start.position.x - transform.position.x > 0 ? 1 : -1;
            yield return MoveRoutine(new Vector2(directionX, 1), _attack1Start.position.x - transform.position.x, _attack1MaxHeight, 1f);
            OnAttack();

            _activeAttack = _attacks[0];
            yield return new WaitForSeconds(0.166f);
            directionX = _attack1End.position.x - transform.position.x > 0 ? 1 : -1;
            yield return MoveRoutine(new Vector2(directionX, 1), _attack1End.position.x - transform.position.x, _attack1MaxHeight, 1f);
        }

        private IEnumerator MoveRoutine(Vector2 direction, float xDelta, float yDelta, float time)
        {
            Vector3 initialPosition = transform.position;

            Vector2 firstHalfPosition = new Vector3(initialPosition.x + (xDelta/2 * direction.x), initialPosition.y + (yDelta * direction.y), initialPosition.z);
            Vector3 endPosition = new Vector3(initialPosition.x + (xDelta * direction.x), initialPosition.y, initialPosition.z);
            float startTime = Time.time;

            while (transform.position != endPosition && (Time.time - startTime) / time <= 1)
            {
                float elapsedTime = (Time.time - startTime) / time;
                if (elapsedTime <= 0.5f)
                {
                    transform.position = Vector3.Lerp(initialPosition, firstHalfPosition, elapsedTime * 2f);
                }
                else
                {
                    transform.position = Vector3.Lerp(firstHalfPosition, endPosition, (elapsedTime - 0.5f) * 2f);
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}