using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManager;
    [SerializeField]
    private GameObject[] _defenders;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "TakeColor")
        {
            collision.gameObject.SetActive(false);
            _gameManager.GetComponent<GameManager>().GenerateShild();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Consentraition")
        {
            this.gameObject.GetComponent<PlayerMove>().enabled = false;
            _gameManager.GetComponent<GameManager>().StartConsentration();
        }
    }

    public void ActiveDefence(Color color)
    {
       
        for (int i = 0; i < _defenders.Length; i++)
        {
            Debug.Log(_defenders[i].activeInHierarchy);
            if (_defenders[i].activeInHierarchy == false)
            {
                _defenders[i].gameObject.SetActive(true);
                _defenders[i].GetComponent<SpriteRenderer>().color = color;
                break;
            }
        }
    }
}
