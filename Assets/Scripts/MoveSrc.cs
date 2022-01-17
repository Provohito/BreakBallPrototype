using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSrc : MonoBehaviour
{
    private Rigidbody2D _rb2D;

    //[SerializeField] private float _speed = 7f;
    
    

    public float PosY { set { positionY = value; SetYPos(); } }
    float positionY;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void SetYPos()
    {
        _rb2D.MovePosition(new Vector2(_rb2D.position.x, positionY));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bg1")
        {
            Destroy(collision.gameObject);
        }
    }

}
