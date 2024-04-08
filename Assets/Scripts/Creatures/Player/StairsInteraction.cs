using System;
using System.Collections;
using System.Collections.Generic;
using Creatures.Player;
using Extensions;
using UnityEngine;

public class StairsInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private float _climbSpeed = 5f;
    private bool _isStairsNear;
    [SerializeField] private bool _isHooked;
    [SerializeField] private LayerMask _stairsMask;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        var inputReader = GetComponent<PlayerInputReader>();
        inputReader.OnMove += PlayerInputReader_OnClimb;
    }

    private void PlayerInputReader_OnClimb(object sender, Vector2 e)
    {
        if (e.y == 0)
        {
            if (!_isHooked) return;
            StopClimb();
        }

        if (!_isHooked) TryHook();
        else
        {
            Climb(e.y * GameManager.Instance.Gravity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.IsInLayer(_stairsMask))
        {
            _isStairsNear = true;
            // TryHook();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.IsInLayer(_stairsMask))
        {
            _isStairsNear = false;
            UnHook();
        }
    }

    private void TryHook()
    {
        if (!_isStairsNear) return;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody.velocity = Vector3.zero;
        _isHooked = true;
    }

    private void UnHook()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _isHooked = false;
    }

    private void Climb(float dir)
    {
        _rigidbody.velocity = Vector2.up * dir * _climbSpeed;
    }

    private void StopClimb()
    {
        _rigidbody.velocity *= Vector2.right;
    }
}