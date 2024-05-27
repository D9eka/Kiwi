using Assets.Scripts.Extensions;
using Creatures;
using UnityEngine;

public class ObjectGravityInverter : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _offset;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (TryGetComponent(out Creature creature))
            _offset = creature.GetOffset();
        else
            _offset = GetComponent<Collider2D>().GetOffset();

        if (MyGameManager.Gravity < 0)
            InvertObjectGravity();
        MyGameManager.OnGravityInverted += (sender, args) => InvertObjectGravity();
    }

    private void InvertObjectGravity()
    {
        if (this == null || !gameObject.activeSelf)
            return;

        transform.position += new Vector3(0, _offset, 0) * -MyGameManager.Gravity;
        transform.Rotate(0, 180, 180);
        _rigidbody.gravityScale = -_rigidbody.gravityScale;
    }

    private void OnDisable()
    {
        MyGameManager.OnGravityInverted -= (sender, args) => InvertObjectGravity();
    }
}