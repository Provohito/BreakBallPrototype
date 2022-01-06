using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _Skins;
    [SerializeField]
    private GameObject _defaultPrefabToSkin;
    [SerializeField]
    private Sprite[] _playerSkins;

    [SerializeField]
    private GameObject _TableSkins;
    private bool _pause = false;



    private GameObject _player;


    private int _countAllSkins = 20;

    [SerializeField]
    private GameObject _parentSellsToSkins;

    private int _currentCountSkins = 3;
    private int _selectedSkin = 1;

    private void Start()
    {
        //PlayerPrefs.SetInt("CurrentCountSkins", 3);
        //PlayerPrefs.SetInt("SelectedSkin", 1);
        //PlayerPrefs.GetInt("SelectedSkin", _selectedSkin);
        //PlayerPrefs.GetInt("CurrentCountSkins", _currentCountSkins);
        CreateSellsToSkins(_selectedSkin);
    }

    private void CreateSellsToSkins(int selectedSkin) // Создание ячейки под скин
    {
        Debug.Log(_currentCountSkins);        
        for (int i = 0; i < _currentCountSkins; i++)
        {
            GameObject prefab = Instantiate(_defaultPrefabToSkin);
            
            prefab.gameObject.transform.SetParent(_parentSellsToSkins.transform);
            prefab.transform.localScale = new Vector3(1, 1, 1);
            prefab.GetComponent<BtnStateSprite>().Skin = _Skins[i];
            prefab.GetComponent<BtnStateSprite>().PlayerSkin = _playerSkins[i];
            if (_selectedSkin == i) // Вызов показа активного спрайта
            {
                prefab.GetComponent<BtnStateSprite>().ActiveSkin = true;
            }
        }

        for (int i = 0; i < _countAllSkins - _currentCountSkins; i++)
        {
            GameObject prefab = Instantiate(_defaultPrefabToSkin);
            prefab.gameObject.transform.SetParent(_parentSellsToSkins.transform);
            prefab.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SelectSkin(GameObject current)
    {
        if (_player != null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _player.GetComponent<SpriteRenderer>().sprite = current.GetComponent<BtnStateSprite>().PlayerSkin;
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pause == false)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
            OpenSkins();
        }
    }

    public void OpenSkins()
    {
        _pause = !_pause;
        _TableSkins.SetActive(_pause);
    }

    bool isSettingsHide;
    public void PressSettinsBtn(GameObject button)
    {
        isSettingsHide = button.GetComponent<Animator>().GetBool("isHide");
        OpenCloseSettings(button.GetComponent<Animator>());
    }

    private void OpenCloseSettings(Animator anim)
    {
        anim.SetBool("isHide", !isSettingsHide);
    }

    public void TakeLevel(int sceneIndex)
    {
        GameObject sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        sceneManager.GetComponent<ChangeScene>().ChooseScene(sceneIndex);
    }
}
