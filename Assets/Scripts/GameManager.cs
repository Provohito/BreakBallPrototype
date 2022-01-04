using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Color[] _colorsLine;

    private Color[] _outColor;
    private int _countLines;

    private int _numberLevel;

    [SerializeField]
    private GameObject _linePrefab;
    [SerializeField]
    private Transform _startPosition;
    [SerializeField]
    private GameObject _uiManager;

    

    private void Start()
    {
        CreateEnemies();
        _numberLevel = 1;
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
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    private void GenerateEnemies(int count)
    {

    }
}
