using Assets.Scripts.Extensions;
using UnityEngine;

namespace Creatures
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Creature : MonoBehaviour
    {
        [SerializeField] protected GameObject _visual;

        [SerializeField] protected bool _invertScale;
        [SerializeField] protected float _speed;

        protected Rigidbody2D _rigidbody;
        protected Animator _animator;

        protected Vector2 _direction;
        protected bool _isGrounded;

        protected const string IS_RUNNING_KEY = "is-running";
        protected const string VERTICAL_VELOCITY_KEY = "vertical-velocity";
        private const string IS_DYING_KEY = "is-dying";

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = _visual.GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            Move();

            UpdateAnimations();
            UpdateSpriteDirection();
        }

        protected virtual void Move()
        {
            _rigidbody.velocity = _direction * _speed + Vector2.down;
        }

        protected virtual void UpdateAnimations()
        {
            _animator.SetFloat(VERTICAL_VELOCITY_KEY, _rigidbody.velocity.y);
            _animator.SetBool(IS_RUNNING_KEY, _direction.x != 0);
        }

        public virtual void UpdateSpriteDirection()
        {
            float multiplier = _invertScale ? -1f : 1f;
            Vector3 localScale = transform.localScale;
            if (_rigidbody.velocity.x > 0)
                transform.localScale = new Vector3(multiplier * Mathf.Abs(localScale.x), localScale.y, localScale.z);
            else if (_rigidbody.velocity.x < 0)
                transform.localScale = new Vector3(-1 * multiplier * Mathf.Abs(localScale.x), localScale.y, localScale.z);
        }

        public virtual void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public float GetOffset()
        {
            return _visual.GetComponent<Collider2D>().GetOffset();
        }

        public virtual void Die()
        {
            _animator.SetTrigger(IS_DYING_KEY);
        }
    }
}