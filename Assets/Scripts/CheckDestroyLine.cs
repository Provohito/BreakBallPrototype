using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDestroyLine : MonoBehaviour
{
    private UIManagerGame _ui;
    private GameManager _gm;
    public void Start()
    {
        _ui = GameObject.Find("UIManagerGame").GetComponent<UIManagerGame>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "defence")
        {
            _gm.GenerateDestroyPoint();
            if (collision.gameObject.GetComponent<SpriteRenderer>().color == this.gameObject.GetComponent<SpriteRenderer>().color)
            {
                collision.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
            else
            {
                _ui.EndGameStart();
            }
        }
        else
            _ui.EndGameStart();
        
    }
}
