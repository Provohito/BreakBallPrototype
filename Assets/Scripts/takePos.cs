using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takePos : MonoBehaviour
{
    [SerializeField]
    private Transform _mainPos;

    private void FixedUpdate()
    {
        transform.position = new Vector3(0.92f, _mainPos.position.y, _mainPos.position.z);
    }
}
