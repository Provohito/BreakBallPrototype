using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChangePos : MonoBehaviour
{
    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _rb.velocity = new Vector2(_rb.velocity.x + 0.004f, _rb.velocity.y);
        if (_rb.velocity.x > 4)
        {
            Destroy(this.gameObject);
        }
    }

    
}
