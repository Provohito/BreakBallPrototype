using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BtnStateSprite : MonoBehaviour
{
    private Sprite _playerSkin;
    public Sprite PlayerSkin
    {
        get { return _playerSkin; }
        set { _playerSkin = value; }
    }

    private int _id;
    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    private Sprite _skin;
    public Sprite Skin 
    {
        get { return _skin; }
        set {_skin = value; SetSkin(); } 
    }

    private bool _activeSkin;
    public bool ActiveSkin { set { _activeSkin = value; SelectedSkin(); } get {return _activeSkin; } }

    [SerializeField]
    private GameObject[] _stateSkins;

    private bool _stateSkin = false;
    public bool StateSkin { set { _stateSkin = value; } get { return _stateSkin; } }

    private void Update()
    {
        if (_activeSkin == true)
        {
            SelectedSkin();
        }
    }

    private void Start()
    {
        
    }
    
    private void SetSkin()
    {
        //GetComponent<Image>().sprite = _skin;
        _stateSkins[0].SetActive(false);
    }

    private void SelectedSkin()
    {
        _stateSkins[1].SetActive(_activeSkin);
    }
}


