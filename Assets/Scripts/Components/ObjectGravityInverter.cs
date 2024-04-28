using UnityEngine;

public class ObjectGravityInverter : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private bool _isNeededOffset;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        MyGameManager.OnGravityInverted += (sender, args) => InvertObjectGravity();
    }

    private void InvertObjectGravity()
    {
        transform.Rotate(0, 180, 180);
        if (_isNeededOffset)
            transform.position += new Vector3(0, transform.lossyScale.y, 0) * MyGameManager.Gravity;
        _rigidbody.gravityScale = MyGameManager.Gravity;
    }
}