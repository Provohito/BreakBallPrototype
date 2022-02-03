using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSkinsSystem : MonoBehaviour
{
    public Skin[] info;
    private bool[] StockCheck;

    public int index;

    public int coins;

    [SerializeField]
    private GameObject _parentSellsToSkins;

    private void Awake()
    {
        index = PlayerPrefs.GetInt("SelectedSkin");
        //coinsText.text = coins.ToString();

        StockCheck = new bool[24];
        if (PlayerPrefs.HasKey("StockArray"))
            StockCheck = PlayerPrefsX.GetBoolArray("StockArray");
        else
        {
            StockCheck[0] = true;
            StockCheck[1] = true;
            StockCheck[2] = true;
        }
            

        info[index].isChosen = true;

        for (int i = 0; i < info.Length; i++)
        {
            info[i].inStock = StockCheck[i];
            if (info[i].inStock == true)
            {
                _parentSellsToSkins.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
                _parentSellsToSkins.transform.GetChild(i).transform.GetChild(2).gameObject.SetActive(true);
                _parentSellsToSkins.transform.GetChild(i).GetComponent<BtnStateSprite>().StateSkin = true;
                _parentSellsToSkins.transform.GetChild(i).GetComponent<BtnStateSprite>().ID = i;
                _parentSellsToSkins.transform.GetChild(i).GetComponent<BtnStateSprite>().Skin = _parentSellsToSkins.transform.GetChild(i).transform.GetChild(2).GetComponent<Image>().sprite;
            }
            if (info[i].inStock && info[i].isChosen)
            {
                _parentSellsToSkins.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
                _parentSellsToSkins.transform.GetChild(i).GetComponent<BtnStateSprite>().ActiveSkin = true;
                
            }
        }
    }

    public void Save(Sprite skin)
    {
        for (int i = 0; i < info.Length; i++)
        {
            if (_parentSellsToSkins.transform.GetChild(i).transform.GetChild(2).GetComponent<Image>().sprite == skin)
            {
                StockCheck[i] = true;
                info[i].inStock = true;
            }  
        }
        PlayerPrefsX.SetBoolArray("StockArray", StockCheck);
    }
    private bool _stateSkin = false;
    public bool StateSkin { get {return _stateSkin; } }

    public void CheckSkin(Sprite skin)
    {
        for (int i = 0; i < info.Length; i++)
        {
            if (_parentSellsToSkins.transform.GetChild(i).transform.GetChild(2).GetComponent<Image>().sprite == skin)
            {
                if (_parentSellsToSkins.transform.GetChild(i).transform.GetChild(2).gameObject.activeInHierarchy == false)
                {
                    StockCheck[i] = true;
                    info[i].inStock = true;
                    _stateSkin = true;
                }
                else
                    _stateSkin = false;
            }
        }

    }
}


[System.Serializable]
public class Skin
{
    public Sprite skin;
    public bool inStock;
    public bool isChosen;
    public int ID;
}

