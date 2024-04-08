using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGravityInverter : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rigidbody;
    [SerializeField] private bool _isNeededOffset;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        GameManager.Instance.OnGravityInverted += (sender, args) => InvertObjectGravity();
    }

    private void InvertObjectGravity()
    {
        transform.Rotate(0, 180, 180);
        if (_isNeededOffset)
            transform.position += new Vector3(0, transform.lossyScale.y, 0) * GameManager.Instance.Gravity;
        _rigidbody.gravityScale = GameManager.Instance.Gravity;
    }
}