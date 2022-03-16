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
                collision.gameObject.SetActive(false);
                indexWall = Random.Range(0,3);
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
                if (_gm.CountLines < 4)
                {
                    switch (_gm.CountLines)
                    {
                        case 1:
                            _ui.Score += 10;
                            break;
                        case 2:
                            _ui.Score += 10;
                            break;
                        case 3:
                            _ui.Score += 8;
                            break;
                        case 4:
                            _ui.Score += 7;
                            break;
                        default:
                            break;
                    }
                }
                _player.GetComponent<AudioSource>().PlayOneShot(_wallBreakSound);
                
                Debug.Log("1");
                _player.GetComponent<PlayerController>().PlayEffect(this.gameObject.GetComponent<SpriteRenderer>().color);
                _ui.Score += 8;
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
