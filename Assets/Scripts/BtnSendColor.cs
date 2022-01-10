using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSendColor : MonoBehaviour
{
    private GameObject _ui;

    private void Start()
    {
        _ui = GameObject.Find("UIManagerGame");
    }
    public void SendColor()
    {
        _ui.GetComponent<UIManagerGame>().PressBtnDefence(this.transform.GetChild(0).GetComponent<Image>().color);
        Destroy(this.gameObject);
    }
}
