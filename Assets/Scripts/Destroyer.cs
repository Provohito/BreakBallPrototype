using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Die")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
