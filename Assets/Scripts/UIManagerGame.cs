using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerGame : MonoBehaviour
{
    private bool _pause = false;

    [SerializeField]
    private Sprite[] _playerSkins;

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

    [SerializeField]
    private GameObject _endGamePanel;
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private TMP_Text _score;
    private float Score = 1;
    private float _time;

    private void Update()
    {
        Debug.Log(_time);
        _time += Time.deltaTime;
        if (_time > 1)
        {
            Score++;
            UpdateScore();
            _time = 0;
        }
    }

    private void UpdateScore()
    {
        _score.text = Score.ToString();
    }

    public void Start()
    {

        _player = GameObject.Find("Player");
        _player.GetComponent<SpriteRenderer>().sprite = _playerSkins[PlayerPrefs.GetInt("SelectedSkin")];
        _score.text = Score.ToString();
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

    [SerializeField] private GameObject _timer;
    [SerializeField] private Image[] timerImage;
    private float _timeLeft = 0f;
    private float time = 2f;
    public void Consentration()
    {
        _consentrationPanel.SetActive(true);
        _timer.SetActive(true);
        for (int i = 0; i < _countDefenceBtn; i++)
        {
            _parentDefencePrefab.GetChild(i).GetComponent<Button>().interactable = true;
        }
        StartTimer();

    }

    private void StartTimer()
    {
        _timeLeft = time;
        StartCoroutine(Timer());
    }

    public void EndGameStart()
    {
        EndGame();
    }

    private void EndGame()
    {
        _gamePanel.SetActive(false);
        _endGamePanel.SetActive(true);
        Time.timeScale = 0;
        _scoreText.text = Score.ToString();

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
        _timer.SetActive(false);
    }

    public void ReloadGame()
    {
        if((int)Score > PlayerPrefs.GetInt("Score"))
            PlayerPrefs.SetInt("Score", (int)Score);

        GameObject.Find("SceneManager").GetComponent<ChangeScene>().ChooseScene(1);
    }

}
