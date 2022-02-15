using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingColorEvent : MonoBehaviour
{
    
    private void Update()
    {
        transform.Rotate(0, 0, 1.0f);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {

        
        if (collision.tag == "Die")
            collision.gameObject.SetActive(false);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Die")
            collision.gameObject.SetActive(false);
    }
}
