using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rb2D;

    private float _moveX = 0f;
    [SerializeField] private float _speed = 7f;


    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        float positionX = _rb2D.position.x + _moveX * _speed * Time.fixedDeltaTime;
        float positionY = _rb2D.position.y + (0.5f * Time.fixedDeltaTime);

        _rb2D.MovePosition(new Vector2(positionX, positionY));
    }

    private void OnEnable()
    {
        PlayerInput.OnMove += Move;
    }

    private void OnDisable()
    {
        PlayerInput.OnMove -= Move;
    }
    private void Move(float moveX)
    {
        _moveX = moveX;
    }
}
