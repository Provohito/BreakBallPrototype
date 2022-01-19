using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManager;
    [SerializeField]
    private GameObject[] _defenders;
    private UIManagerGame _ui;
    [SerializeField]
    private AudioSource[] _audio;

    private bool isAudioOn = false;
    private bool isMusikOn = true;

    private void Start()
    {
       
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        _ui = GameObject.Find("UIManagerGame").GetComponent<UIManagerGame>();
        if (collision.transform.tag == "TakeColor")
        {
            _audio[1].Play();
            collision.gameObject.SetActive(false);
            _gameManager.GetComponent<GameManager>().GenerateShild();
            transform.GetChild(5).GetComponent<ParticleSystem>().Play();
        }
        if (collision.transform.tag == "Die")
        {
            Debug.Log("Die");
            _ui.EndGameStart();
        }
    }

    public void PlaySoundDestroyWall()
    {
        if (_audio[2].isActiveAndEnabled)
        {
            _audio[2].Play();
        } 
    }

    public void SoundCheck(GameObject currentGameobject)
    {
        for (int i = 0; i < _audio.Length; i++)
        {
            _audio[i].enabled = isAudioOn;
        }
        isAudioOn = !isAudioOn;
    }

    public void MusikCheck(GameObject currentGameobject)
    {
        Camera.main.transform.GetChild(1).GetComponent<AudioSource>().mute = isMusikOn;
        isMusikOn = !isMusikOn;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject _defPen = GameObject.Find("DefencePanel");
        if (collision.transform.tag == "Consentraition")
        {
            _audio[0].Play();
            if (_defPen.transform.childCount == 0)
            {
                _ui.EndGameStart();
            }
            
            this.gameObject.GetComponent<PlayerMove>().enabled = false;
            transform.GetChild(4).GetComponent<ParticleSystem>().Play();
            this.gameObject.transform.position = collision.transform.position;
            _gameManager.GetComponent<GameManager>().StartConsentration();
            
            Destroy(collision.gameObject);
        }
    }

    public void PlayEffect(Color color)
    {
        ParticleSystem.MainModule main = transform.GetChild(2).GetComponent<ParticleSystem>().main;
        main.startColor = color;
        transform.GetChild(2).GetComponent<ParticleSystem>().Play();
    }

    public void ActiveDefence(Color color)
    {
       
        for (int i = 0; i < _defenders.Length; i++)
        {
            if (_defenders[i].activeInHierarchy == false)
            { 
                _defenders[i].gameObject.SetActive(true);
                _defenders[i].GetComponent<SpriteRenderer>().color = color;
                _defenders[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                break;
            }
        }
    }
}
