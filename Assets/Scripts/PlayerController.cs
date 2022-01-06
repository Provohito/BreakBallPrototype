using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManager;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "TakeColor")
        {
            collision.gameObject.SetActive(false);
            _gameManager.GetComponent<GameManager>().GenerateShild();
        }
        if (collision.transform.tag == "Consentraition")
        {
            Time.timeScale = 0.1f;
        }
    }
}
