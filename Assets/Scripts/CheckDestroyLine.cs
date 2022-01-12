using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDestroyLine : MonoBehaviour
{
    private UIManagerGame _ui;
    public void Start()
    {
        _ui = GameObject.Find("UIManagerGame").GetComponent<UIManagerGame>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "defence")
        {
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
        
    }
}
