using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSendColor : MonoBehaviour
{
    private GameObject _ui;
    private GameObject _defenceConteiner;

    private void Start()
    {
        _ui = GameObject.Find("UIManagerGame");
        _defenceConteiner = GameObject.Find("DefencePanel");
    }
    public void SendColor()
    {
        if (_defenceConteiner.transform.childCount == 1)
        {
            _ui.GetComponent<UIManagerGame>().GoGame();
        }
        _ui.GetComponent<UIManagerGame>().PressBtnDefence(this.transform.GetChild(0).GetComponent<Image>().color);
        Destroy(this.gameObject);
    }
}
