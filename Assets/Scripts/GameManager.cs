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

    public void TakeLevel(int sceneIndex)
    {
        GameObject sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        sceneManager.GetComponent<ChangeScene>().ChooseScene(sceneIndex);
    }

    private void Start()
    {
        CreateEnemies();
    }

    private void CreateEnemies()
    {
        switch (_numberLevel)
        {
            case 1:
                Generateenemies(_numberLevel);
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }

    private void Generateenemies(int count)
    {

    }
}
