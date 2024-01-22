using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;

    private Vector2 _movementDriection = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();  
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ApplyMovement(_movementDriection);
    }

    private void Move(Vector2 direction)
    {
        _movementDriection = direction;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * 5;

        _rigidbody.velocity = direction;
    }
}
