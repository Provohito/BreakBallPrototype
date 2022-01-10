using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private int _countDefenceBtn = 0;
    private GameObject _player;

    [SerializeField]
    private GameObject _consentrationPanel;

    public void Start()
    {
        _player = GameObject.Find("Player");
    }

    public void PressBtnDefence(Color color)
    {
        _player.GetComponent<PlayerController>().ActiveDefence(color);
    }

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
        _countDefenceBtn = count;
    }


    private void InitDefenceBtn(Color[] colors, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = Instantiate(_defenceBtnPrefab);
            prefab.transform.SetParent(_parentDefencePrefab);
            prefab.transform.GetChild(0).GetComponent<Image>().color = colors[i];
        }
    }


    [SerializeField] private Image[] timerImage;
    private float _timeLeft = 0f;
    private float time = 2f;
    public void Consentration()
    {
        _consentrationPanel.SetActive(true);
        StartTimer();
        for (int i = 0; i < _countDefenceBtn; i++)
        {
            _parentDefencePrefab.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    private void StartTimer()
    {
        _timeLeft = time;
        StartCoroutine(Timer());
    }

  
    private IEnumerator Timer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            var normalizedValue = Mathf.Clamp(_timeLeft / time, 0.0f, 1.0f);
            timerImage[0].fillAmount = normalizedValue;
            timerImage[1].fillAmount = normalizedValue;
            yield return null;
        }
        
        _player.GetComponent<PlayerMove>().enabled = true;
        _consentrationPanel.SetActive(false);
    }

}
