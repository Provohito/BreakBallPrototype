using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private int _countAllSkins = 24;
    [SerializeField]
    private GameObject _parentSellsToSkins;

    private int _currentCountSkins = 23;
    private int _selectedSkin;

    [SerializeField]
    private TMP_Text _score;
    private GameObject _player;

    [SerializeField]
    private AudioClip _changeSkinSound;
    [SerializeField]
    private AudioClip _btnPress;

    private void Start()
    {
        Time.timeScale = 1;
        _player = GameObject.Find("Player");
        if (PlayerPrefs.HasKey("Score") == true)
        {
            _score.text = PlayerPrefs.GetInt("Score", 0).ToString();
        }

        if (PlayerPrefs.HasKey("SelectedSkin") == true)
        {
            _selectedSkin = PlayerPrefs.GetInt("SelectedSkin");
            _player.GetComponent<SpriteRenderer>().sprite = _playerSkins[_selectedSkin];
        }
        else
            _selectedSkin = 1;

        
        //PlayerPrefs.SetInt("CurrentCountSkins", 3);       !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Важно, сколько скинов открыли !!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //CreateSellsToSkins(_selectedSkin);
    }

    public void PressBtnSound()
    {
        _player = GameObject.Find("Player");
        _player.GetComponent<AudioSource>().PlayOneShot(_btnPress);
    }

    private void CreateSellsToSkins(int selectedSkin) // Создание ячейки под скин
    {      
        for (int i = 0; i < _currentCountSkins; i++)
        {
            GameObject prefab = Instantiate(_defaultPrefabToSkin, new Vector3(1,1,0), Quaternion.identity);
            
            prefab.transform.SetParent(_parentSellsToSkins.transform, false);
            prefab.transform.localScale = new Vector3(1, 1, 1);
            prefab.GetComponent<BtnStateSprite>().Skin = _Skins[i];
            prefab.GetComponent<BtnStateSprite>().PlayerSkin = _playerSkins[i];
            prefab.GetComponent<BtnStateSprite>().ID = i;
            if (_selectedSkin == i) // Вызов показа активного спрайта
            {
                prefab.GetComponent<BtnStateSprite>().ActiveSkin = true;
            }
            prefab.GetComponent<BtnStateSprite>().StateSkin = true;
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
        
        _parentSellsToSkins = GameObject.Find("Content");
        if (current.GetComponent<BtnStateSprite>().StateSkin == false)
        {
            Debug.Log("return");
            return;
        }
        if (current.GetComponent<BtnStateSprite>().ActiveSkin == true)
        {
            return;
        }
        else
        {
            
            current.GetComponent<BtnStateSprite>().ActiveSkin = true;
            _parentSellsToSkins.transform.GetChild(_selectedSkin).GetComponent<BtnStateSprite>().ActiveSkin = false;
            _selectedSkin = current.GetComponent<BtnStateSprite>().ID;
            PlayerPrefs.SetInt("SelectedSkin", _selectedSkin);
            _player = GameObject.Find("Player");
            _player.GetComponent<AudioSource>().PlayOneShot(_changeSkinSound);
            _player.GetComponent<SpriteRenderer>().sprite = _playerSkins[_selectedSkin];
        }
        
    }

    private void Update()
    {

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool _stateMusik = false;
    public void OnOffMusic()
    {
        _stateMusik = !_stateMusik;
        GameObject _sm = GameObject.Find("SoundManager");
        _sm.GetComponent<AudioSource>().mute = _stateMusik;
    }
}
