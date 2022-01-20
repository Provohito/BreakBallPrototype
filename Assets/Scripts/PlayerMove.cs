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

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        

        if (_rb2D.position.x > Screen.width/2)
        {
            _rb2D.position = new Vector3(2.42f, _rb2D.position.y);
        }
        else if (_rb2D.position.x < -Screen.width/2)
        {
            _rb2D.position = new Vector3(-2.42f, _rb2D.position.y);
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
