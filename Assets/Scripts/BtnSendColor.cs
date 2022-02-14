using System.Collections;
using System.Collections.Generic;
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
    public void SendColor()
    {
        audio.PlayOneShot(_pressBtn);
        if (_defenceConteiner.transform.childCount == 1)
        {
            _ui.GetComponent<UIManagerGame>().GoGame();
        }
        _ui.GetComponent<UIManagerGame>().PressBtnDefence(this.transform.GetChild(0).GetComponent<Image>().color);
        _effectDestroy = GameObject.Find("EffectDestroyDefence");
        _effectDestroy.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _effectDestroy.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        Destroy(this.gameObject);
    }


}

