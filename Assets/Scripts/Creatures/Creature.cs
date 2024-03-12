using Components.ColliderBased;
using UnityEngine;

namespace Creatures
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Creature : MonoBehaviour
    {
        [SerializeField] protected GameObject _visual;

        [SerializeField] protected bool _invertScale;
        [SerializeField] protected float _speed;

        [SerializeField] protected float _jumpForce;

        [Header("Checkers")]
        [SerializeField] protected LayerCheck _groundCheck;

        protected Rigidbody2D _rigidbody;
        protected Animator _animator;

        protected Vector2 direction;
        protected bool isGrounded;

        protected const string IS_ON_GROUND_KEY = "is-on-ground";
        protected const string IS_RUNNING_KEY = "is-running";
        protected const string VERTICAL_VELOCITY_KEY = "vertical-velocity";
        private const string DIE_KEY = "death";

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = _visual.GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            isGrounded = _groundCheck.IsTouchingLayer;

            Move();
            Jump();

            UpdateAnimations();
            UpdateSpriteDirection();
        }

        protected virtual void Move()
        {
            var xVelocity = direction.x * _speed;
            _rigidbody.velocity = new Vector2(xVelocity, _rigidbody.velocity.y);
        }

        protected virtual void Jump()
        {
            bool isJumpKeyPressed = direction.y > 0;

            if (isGrounded && isJumpKeyPressed)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
                return;
            }
        }

        protected virtual void UpdateAnimations()
        {
            _animator.SetFloat(VERTICAL_VELOCITY_KEY, _rigidbody.velocity.y);
            _animator.SetBool(IS_ON_GROUND_KEY, isGrounded);
            _animator.SetBool(IS_RUNNING_KEY, direction.x != 0);
        }

        public virtual void UpdateSpriteDirection()
        {
            var multiplier = _invertScale ? -1 : 1;
            if (direction.x > 0)
                transform.localScale = new Vector3(multiplier, 1, 1);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-multiplier, 1, 1);
        }

        public virtual void SetDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        public virtual void Die()
        {
            _animator.SetTrigger(DIE_KEY);
        }
    }
}