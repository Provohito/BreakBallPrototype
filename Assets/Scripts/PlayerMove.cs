using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rb2D;

    private float _moveX = 0f;
    private float _speed = 2f;
    public float Speed { get {return _speed ;} set {_speed = value; } }

    [SerializeField] private GameObject _movePoint;
    private Vector3 min;
    private Vector3 max;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }


    private void FixedUpdate()
    {
        

        if (_rb2D.position.x > max.x)
        {
            _rb2D.position = new Vector3(max.x-0.01f, _rb2D.position.y);
        }
        else if (_rb2D.position.x < min.x)
        {
            _rb2D.position = new Vector3(min.x+0.05f, _rb2D.position.y);
        }
        float positionX = _rb2D.position.x + _moveX * 7f * Time.fixedDeltaTime;
        float positionY = _rb2D.position.y + _speed * Time.fixedDeltaTime;

        _movePoint.GetComponent<MoveSrc>().PosY = positionY;
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
