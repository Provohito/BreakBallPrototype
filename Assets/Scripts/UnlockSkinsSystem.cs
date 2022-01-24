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

    private void Save()
    {
        PlayerPrefsX.SetBoolArray("StockArray", StockCheck);
    }
    /*
    public void ScrollRight()
    {
        if (index < player.childCount)
        {
            index++;

            if (info[index].inStock && info[index].isChosen)
            {
                priceText.text = "CHOSEN";
                buyBttn.interactable = false;
            }
            else if (!info[index].inStock)
            {
                //priceText.text = info[index].cost.ToString();
                buyBttn.interactable = true;
            }
            else if (info[index].inStock && !info[index].isChosen)
            {
                priceText.text = "CHOOSE";
                buyBttn.interactable = true;
            }

            for (int i = 0; i < player.childCount; i++)
                player.GetChild(i).gameObject.SetActive(false);
            // Можно записать так: player.GetChild(index-1).gameObject.SetActive(false);

            player.GetChild(index).gameObject.SetActive(true);
        }
    }

    public void ScrollLeft()
    {
        if (index > 0)
        {
            index--;

            if (info[index].inStock && info[index].isChosen)
            {
                priceText.text = "CHOSEN";
                buyBttn.interactable = false;
            }
            else if (!info[index].inStock)
            {
                //priceText.text = info[index].cost.ToString();
                buyBttn.interactable = true;
            }
            else if (info[index].inStock && !info[index].isChosen)
            {
                priceText.text = "CHOOSE";
                buyBttn.interactable = true;
            }

            for (int i = 0; i < player.childCount; i++)
                player.GetChild(i).gameObject.SetActive(false);

            player.GetChild(index).gameObject.SetActive(true);
        }
    }

    public void BuyButtonAction()
    {
        if (buyBttn.interactable && !info[index].inStock)
        {
            if (coins > int.Parse(priceText.text))
            {
                coins -= int.Parse(priceText.text);
                coinsText.text = coins.ToString();
                PlayerPrefs.SetInt("coins", coins);
                StockCheck[index] = true;
                info[index].inStock = true;
                priceText.text = "CHOOSE";
                Save();
            }
        }

        if (buyBttn.interactable && !info[index].isChosen && info[index].inStock)
        {
            PlayerPrefs.SetInt("chosenSkin", index);
            buyBttn.interactable = false;
            priceText.text = "CHOSEN";
        }
    }
    */
}


[System.Serializable]
public class Skin
{
    public Sprite skin;
    public bool inStock;
    public bool isChosen;
    public int ID;
}

