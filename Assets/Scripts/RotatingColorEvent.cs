using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingColorEvent : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 0, 1.0f);
    }
}
