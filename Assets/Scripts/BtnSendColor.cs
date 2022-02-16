using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BtnSendColor : MonoBehaviour
{
    private GameObject _ui;
    private GameObject _defenceConteiner;
    private GameObject _effectDestroy;

    [SerializeField]
    private AudioClip _pressBtn;
    private new AudioSource audio;

    private void Start()
    {
        _ui = GameObject.Find("UIManagerGame");
        _defenceConteiner = GameObject.Find("DefencePanel");
        audio = GameObject.Find("Player").GetComponent<AudioSource>();
    }
    int _fullContainer = 0;
    [System.Obsolete]
    public void SendColor()
    {
        
        audio.PlayOneShot(_pressBtn);
        
        if (true)
        {

        }
        _ui.GetComponent<UIManagerGame>().PressBtnDefence(this.transform.GetChild(0).GetComponent<Image>().color);
        _effectDestroy = GameObject.Find("EffectDestroyDefence");
        _effectDestroy.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _effectDestroy.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<Button>().interactable = false;
        if (_defenceConteiner.transform.childCount == 1 & _defenceConteiner.transform.GetChild(0).GetComponent<Button>().interactable == false)
        {
            _ui.GetComponent<UIManagerGame>().DestroyShield();
            _ui.GetComponent<UIManagerGame>().GoGame();
        }
        for (int i = 0; i < _defenceConteiner.transform.childCount; i++)
        {
            if (_defenceConteiner.transform.GetChild(i).GetComponent<Button>().interactable == false)
            {
                _fullContainer++;
            }
        }
        if (_fullContainer == _defenceConteiner.transform.childCount)
        {
            _ui.GetComponent<UIManagerGame>().DestroyShield();
            _ui.GetComponent<UIManagerGame>().GoGame();
        }
        
        Color col;
        col = new Color(1, 1, 1, 0);
        gameObject.transform.GetChild(0).GetComponent<Image>().color = col;
        gameObject.transform.GetChild(1).GetComponent<Image>().color = col;
    }


}

