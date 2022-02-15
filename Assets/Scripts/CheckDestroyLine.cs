using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDestroyLine : MonoBehaviour
{
    private UIManagerGame _ui;
    private GameManager _gm;
    private GameObject _player;
    private int indexWall = 0;

    [SerializeField]
    private AudioClip _wallBreakSound;
    public void Start()
    {
        _ui = GameObject.Find("UIManagerGame").GetComponent<UIManagerGame>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "defence")
        {
            
            if (collision.gameObject.GetComponent<SpriteRenderer>().color == this.gameObject.GetComponent<SpriteRenderer>().color)
            {
                indexWall = int.Parse(collision.gameObject.name);
                Debug.Log(indexWall);
                switch (indexWall)
                {
                    case 0:
                        _player.GetComponent<AudioSource>().pitch = 0.8f;
                        break;
                    case 1:
                        _player.GetComponent<AudioSource>().pitch = 1;
                        break;
                    case 2:
                        _player.GetComponent<AudioSource>().pitch = 1.05f;
                        break;
                    case 3:
                        _player.GetComponent<AudioSource>().pitch = 1.15f;
                        break;
                    default:
                        _player.GetComponent<AudioSource>().pitch = 1f;
                        break;
                }
                _player.GetComponent<AudioSource>().PlayOneShot(_wallBreakSound);
                collision.gameObject.SetActive(false);
                _player.GetComponent<PlayerController>().PlayEffect(this.gameObject.GetComponent<SpriteRenderer>().color);
                _ui.Score += 10;
                Destroy(this.gameObject);
            }
            else
            {
                _ui.EndGameStart();
            }
        }
        else if(collision.tag == "Player")
        {
            _ui.EndGameStart();
        }

    }
}
