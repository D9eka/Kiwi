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
        private const string DEATH_TRIGGER = "death";

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
            var multiplier = _invertScale ? -1 : 1;
            if (_direction.x > 0)
                transform.localScale = new Vector3(multiplier, 1, 1);
            else if (_direction.x < 0)
                transform.localScale = new Vector3(-multiplier, 1, 1);
        }

        public virtual void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public virtual void Die()
        {
            _animator.SetTrigger(DEATH_TRIGGER);
        }
    }
}