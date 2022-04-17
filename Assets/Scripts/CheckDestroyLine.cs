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

        _player = GameObject.Find("Player");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (collision.tag == "defence")
        {
            Debug.Log("1234");
            if (collision.gameObject.GetComponent<SpriteRenderer>().color == this.gameObject.GetComponent<SpriteRenderer>().color)
            {
                Debug.Log("1234453");
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
                _player.transform.GetChild(7).transform.position = collision.transform.position;
                _player.transform.GetChild(8).transform.position = collision.transform.position;
                _player.transform.GetChild(7).GetComponent<ParticleSystem>().Play();
                _player.transform.GetChild(8).GetComponent<ParticleSystem>().Play();
                _player.GetComponent<PlayerController>().PlayEffect(this.gameObject.GetComponent<SpriteRenderer>().color);
                _ui.Points += 8;
                Destroy(this.gameObject);
            }
            else
                _ui.EndGameStart();


        }
        else if(collision.tag == "Player")
        {
            collision.GetComponent<SpriteRenderer>().enabled = false;
            _ui.EndGameStart();
        }
    }
}
