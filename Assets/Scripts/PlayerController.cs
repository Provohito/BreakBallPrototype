using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManager;
    [SerializeField]
    private GameObject[] _defenders;
    private UIManagerGame _ui;
    [SerializeField]
    private AudioSource _audio;



    private bool isMusikOn = false;
    private bool isAudioOn = false;

    [SerializeField]
    private Sprite[] _audioSprites;

    private UnlockSkinsSystem _unSys;

    private void Start()
    {
        _unSys = GameObject.Find("SkinChanger").GetComponent<UnlockSkinsSystem>();
        if (!PlayerPrefs.HasKey("MusikOn"))
        {
            PlayerPrefs.SetInt("MusikOn", 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("MusikOn") == 1)
            {
                isMusikOn = true;
            }   
            else
            {
                isMusikOn = false;
                MusikCheck();
            }
            
        }
        if (!PlayerPrefs.HasKey("SoundsOn"))
        {
            PlayerPrefs.SetInt("SoundsOn", 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("SoundsOn") == 1)
            {
                isAudioOn = true;
            }
            else
            {
                isAudioOn = false;
                SoundCheck();
            }
            
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        _ui = GameObject.Find("UIManagerGame").GetComponent<UIManagerGame>();
        if (collision.transform.tag == "TakeColor")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().NumberLevel++;
            GameObject.Find("GameManager").GetComponent<GameManager>().CountLines++;
            collision.gameObject.SetActive(false);
            _gameManager.GetComponent<GameManager>().GenerateShild();
            transform.GetChild(5).GetComponent<ParticleSystem>().Play();
            this.gameObject.GetComponent<PlayerMove>().enabled = false;
            _gameManager.GetComponent<GameManager>().StartConsentration();
        }
        if (collision.transform.tag == "Die")
        {
            collision.gameObject.SetActive(false);
            _ui.EndGameStart();
        }
    }

    public void SoundCheck()
    {
        
        SwapSprite(0);
        _audio.enabled = isAudioOn;
        isAudioOn = !isAudioOn;
        if (isAudioOn)
            PlayerPrefs.SetInt("SoundsOn", 1);
        else
        {
            PlayerPrefs.SetInt("SoundsOn", 0);
        }

    }


    public void MusikCheck()
    {
        GameObject _musik = GameObject.Find("DefaultMusik");
        SwapSprite(1);
        _musik.transform.GetChild(0).GetComponent<AudioSource>().mute = isMusikOn;
        isMusikOn = !isMusikOn;
        
        if (isMusikOn)
            PlayerPrefs.SetInt("SoundsOn", 1);
        else
        {
            PlayerPrefs.SetInt("SoundsOn", 0);
        }

    }

    private void SwapSprite(int index)
    {
        GameObject sound = GameObject.Find("Sound");
        GameObject musik = GameObject.Find("Musik");
        if (index == 0)
        {

            if (sound.GetComponent<Image>().sprite.name == "Sound")
            {
                sound.GetComponent<Image>().sprite = _audioSprites[1];
            }
            else
                sound.GetComponent<Image>().sprite = _audioSprites[0];
        }
        else
        {
            if (musik.GetComponent<Image>().sprite.name == "Musik")
            {
                musik.GetComponent<Image>().sprite = _audioSprites[3];
            }
            else
                musik.GetComponent<Image>().sprite = _audioSprites[2];
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
