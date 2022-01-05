using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerGame : MonoBehaviour
{
    private bool _pause = false;

    [SerializeField]
    private GameObject _pausePanel;
    [SerializeField]
    private GameObject _gamePanel;

    [SerializeField]
    private GameObject _defenceBtnPrefab;
    [SerializeField]
    private Transform _parentDefencePrefab;

    


    public void PressPause()
    {
        if (_pause == false)
        {
            _pausePanel.SetActive(true);
            _gamePanel.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            _pausePanel.SetActive(false);
            _gamePanel.SetActive(true);
            Time.timeScale = 1;
        }
        _pause = !_pause;
    }

    public void PressInitDefenceBtn(Color[] colors, int count)
    {
        InitDefenceBtn(colors, count);
    }


    private void InitDefenceBtn(Color[] colors, int count)
    {

    }


    public void TakeLevel(int sceneIndex)
    {
        GameObject sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        sceneManager.GetComponent<ChangeScene>().ChooseScene(sceneIndex);
    }
}
