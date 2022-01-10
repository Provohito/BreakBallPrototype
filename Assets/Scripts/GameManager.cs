using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Color[] _colorsLine;

    private Color[] _outColor = new Color[4];
    private int _countLines;

    private int _numberLevel;

    [SerializeField]
    private GameObject _linePrefab;
    [SerializeField]
    private Transform _startPosition;
    [SerializeField]
    private GameObject _uiManager;

    private bool _gameContinue = false;

    

    private void Start()
    {
        _numberLevel = 1;
        GenerateContainer();
        
        //StartGame();
    }

    private void Update()
    {
        if (_gameContinue == true)
            StartGame();
    }

    private void StartGame()
    {
        
        CreateEnemies();
        
    }

    private void CreateEnemies()
    {
        switch (_numberLevel)
        {
            
            case 1:
                GenerateEnemies(_numberLevel);
                _countLines = _numberLevel;
                break;
            case 2:
                GenerateEnemies(_numberLevel);
                _countLines = _numberLevel;
                break;
            case 3:
                GenerateEnemies(_numberLevel);
                _countLines = _numberLevel;
                break;
            case 4:
                GenerateEnemies(_numberLevel);
                _countLines = _numberLevel;
                break;
        }
    }

    private void GenerateEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = Instantiate(_linePrefab);
            prefab.GetComponent<SpriteRenderer>().color = _outColor[i];
            prefab.transform.position = new Vector3(_linePrefab.transform.position.x, (_linePrefab.transform.position.y + 10 + i)/7, _linePrefab.transform.position.z);
        }
        
    }

    [SerializeField]
    private GameObject _colorsConteiner;

    private void GenerateContainer()
    {
        GameObject mainColorPref;
        GameObject colorPref;
        mainColorPref = _colorsConteiner.transform.GetChild(_numberLevel-1).gameObject;
        mainColorPref.SetActive(true);
        for (int i = 0; i < _numberLevel; i++)
        {
            _outColor[i] = _colorsLine[i];
            colorPref = mainColorPref.transform.GetChild(i).gameObject;
            colorPref.GetComponent<SpriteRenderer>().color = _outColor[i];
        }
        CreateEnemies();
    }

    public void GenerateShild()
    {
        _uiManager.GetComponent<UIManagerGame>().PressInitDefenceBtn(_outColor,_numberLevel);
    }

    public void StartConsentration()
    {
        _uiManager.GetComponent<UIManagerGame>().Consentration();
    }
}
