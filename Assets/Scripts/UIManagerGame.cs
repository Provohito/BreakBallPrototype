using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerGame : MonoBehaviour
{
    private bool _pause = false;

    [SerializeField]
    private Sprite[] _playerSkins;

    [SerializeField] private GameManager _GM;

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
    public float Score { get {return score; } set {score = value; UpdateScore(); } }
    private float score = 1;
    private float _previousScore = 5;
    private float _time;

    [SerializeField]
    private Sprite[] _pointSkins;
    [SerializeField]
    private UnlockSkinsSystem _unSystem;

    //private int decster = 20;
    
    public int Points { get { return _points; } set { _points = value; UpdateScore();} }
    private int _points = 0;


    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > 1)
        {
            score++;
            UpdateScore();
            _time = 0;
        }

        

    }

    public void ApplicationClose()
    {
        Application.Quit();
    }
    
    

    private void UpdateScore()
    {
        _score.text = ((int)(_player.transform.position.y) + _points).ToString() + " M";
        if (_GM.NumberLevel == 2)
        {
            
        }
        if (score - _previousScore >= 15)
        {
            StopAllCoroutines();
            _GM.NextLevel();
            _previousScore = score;
        }

        switch (score)
        {
            case 30:
                _unSystem.Save(_pointSkins[0]);
                break;
            case 50:
                _unSystem.Save(_pointSkins[1]);
                break;
            case 70:
                _unSystem.Save(_pointSkins[2]);
                break;
            case 90:
                _unSystem.Save(_pointSkins[3]);
                break;
            case 110:
                _unSystem.Save(_pointSkins[4]);
                break;
            case 130:
                _unSystem.Save(_pointSkins[5]);
                break;
            case 150:
                _unSystem.Save(_pointSkins[6]);
                break;
        }
    }

    public void Start()
    {
        Time.timeScale = 1;
        _player = GameObject.Find("Player");
        _player.GetComponent<SpriteRenderer>().sprite = _playerSkins[PlayerPrefs.GetInt("SelectedSkin")];
        _score.text = 0 + " M";
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
            if (isConsentration == true)
            {
                _pausePanel.SetActive(false);
                _gamePanel.SetActive(true);
                Time.timeScale = 0.3f;
            }
            else
            {
                _pausePanel.SetActive(false);
                _gamePanel.SetActive(true);
                Time.timeScale = 1;
            }
            
        }
        _pause = !_pause;
    }

    private int countDefence;
    public void PressInitDefenceBtn(Color[] colors, int count)
    {
        countDefence = count;
        _countDefenceBtn = count;
        InitDefenceBtn(colors, count);
        
        
    }
    private int[] indexs;

    private void InitDefenceBtn(Color[] colors, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = Instantiate(_defenceBtnPrefab,new Vector3(1,1,0), Quaternion.identity);
            
            prefab.transform.SetParent(_parentDefencePrefab,false);
            prefab.transform.localScale = new Vector3(1, 1, 1);
            prefab.transform.GetChild(0).GetComponent<Image>().color = colors[i];
            
        }
        Shuffle(_countDefenceBtn);
    }

    private void Shuffle(int block)
    {
        for (int i = 0; i < block; i++)
        {
            Color temp = _parentDefencePrefab.GetChild(i).GetChild(0).GetComponent<Image>().color;
            int r = Random.Range(0, block);
            _parentDefencePrefab.GetChild(i).GetChild(0).GetComponent<Image>().color = _parentDefencePrefab.GetChild(r).GetChild(0).GetComponent<Image>().color;
            _parentDefencePrefab.GetChild(r).GetChild(0).GetComponent<Image>().color = temp;
        }
        
    }

    [SerializeField] private GameObject _timer;
    [SerializeField] private Image[] timerImage;
    [SerializeField]
    private GameObject _blackWindow;
    private float _timeLeft = 0f;
    private float time = 1.5f;
    [SerializeField]
    private ChangeScene _chengeSceneSrc;
    private bool isConsentration = false;
    public void Consentration()
    {
        isConsentration = true;
        for (int i = 0; i < _countDefenceBtn; i++)
        {
            _parentDefencePrefab.GetChild(i).GetComponent<Button>().interactable = true;
        }
        Time.timeScale = 0.3f;
        _consentrationPanel.SetActive(true);
        _timer.SetActive(true);
        StartTimer();
        
    }

    private void StartTimer()
    {
        _timeLeft = time;
        _blackWindow.SetActive(true);
        StartCoroutine(Timer());
    }

    [SerializeField]
    private AudioClip _depthSound;

    public void EndGameStart()
    {
        for (int i = 0; i < _parentDefencePrefab.childCount; i++)
        {
            Destroy(_parentDefencePrefab.GetChild(i).gameObject);
        }
        _player.GetComponent<AudioSource>().PlayOneShot(_depthSound);
        _player.transform.GetChild(6).GetComponent<ParticleSystem>().Play();
        StartCoroutine(WaitTime());
    }
    IEnumerator WaitTime()
    {
        _chengeSceneSrc.WaitGame();
        yield return new WaitForSeconds(1.4f);
        EndGame();
    }

    private int resultScore = 0;
    [SerializeField]
    private AudioClip _endGameCheck;
    [SerializeField]
    private AudioClip _fireWorkSound;
    private void EndGame()
    {
        
        _GM.gameObject.SetActive(false);
        _gamePanel.SetActive(false);
        _endGamePanel.SetActive(true);
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < _enemies.Length; i++)
        {

            Destroy(_enemies[i]);
        }
        
        StartCoroutine(ScoreCheck());
        _player.GetComponent<AudioSource>().PlayOneShot(_endGameCheck);
    }
    [SerializeField]
    private GameObject _prePlayer;
    public void GGame()
    {
        Debug.Log("GGame");
        //_player.GetComponent<PlayerController>().SCheck();
        //_player.GetComponent<PlayerController>().MCheck();
        Time.timeScale = 1;
        _player.GetComponent<SpriteRenderer>().enabled = true;
        StopAllCoroutines();
        _scorePanel.SetActive(false);
        _gamePanel.SetActive(true);
        _endGamePanel.SetActive(false);
        _prePlayer.SetActive(true);
        _GM.gameObject.SetActive(true);
        
        _GM.NextLevel();
        _player.GetComponent<Rigidbody2D>().constraints = (RigidbodyConstraints2D)RigidbodyConstraints.None;
        _player.GetComponent<Rigidbody2D>().freezeRotation = true;
        for (int i = 0; i < _player.transform.GetChild(0).childCount; i++)
        {
            _player.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
    }

    private float timeToCheck = 2.8f;
    [SerializeField]
    private GameObject _scorePanel;
    private IEnumerator ScoreCheck()
    {
        while (timeToCheck > 0)
        {
            timeToCheck -= 0.05f;

            _scoreText.text = resultScore.ToString();
            resultScore += 1;
            yield return null;
        }
        _scoreText.text = score.ToString();
        _player.GetComponent<AudioSource>().Stop();
        if ((int)score > PlayerPrefs.GetInt("Score"))
        {
            _scorePanel.SetActive(true);
            _scorePanel.GetComponent<Animator>().Play("NewRecord");
            _player.GetComponent<AudioSource>().PlayOneShot(_fireWorkSound);
        }    
            
        
        

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
        
        GoGame();
    }

    public void DestroyShield()
    {
        _parentDefencePrefab.GetComponent<VerticalLayoutGroup>().enabled = false;
        for (int i = 0; i < _parentDefencePrefab.transform.childCount; i++)
        {
            GameObject reff = _parentDefencePrefab.GetChild(i).gameObject;
            Destroy(reff);
        }
        _parentDefencePrefab.GetComponent<VerticalLayoutGroup>().enabled = true;
    }

    public void GoGame()
    {
        isConsentration = false;
        Time.timeScale = 1;
        _blackWindow.SetActive(false);
        _player.GetComponent<PlayerMove>().enabled = true;
        _consentrationPanel.SetActive(false);
        _timer.SetActive(false);
    }

    public void ReloadGame()
    {
        if((int)score > PlayerPrefs.GetInt("Score"))
            PlayerPrefs.SetInt("Score", (int)score);

        GameObject _chG = GameObject.Find("ChangeGrad");
        _chG.GetComponent<ChangeGradient>().NewGameStart();
        GameObject.Find("SceneManager").GetComponent<ChangeScene>().ChooseScene(0);
    }

    
}
