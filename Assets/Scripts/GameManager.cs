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

    [SerializeField]
    private GameObject _consentrationPrefab;

    [SerializeField]
    private GameObject[] _particleEffectsConteiners;
    [SerializeField]
    private Sprite[] _particleSkins;
    private bool _cosmos;

    private int _speedLevel;
    

    [SerializeField] private GameObject _player;

    private void Start()
    {
        _numberLevel = 1;
        GenerateContainer();
        GenerateDestroyer();
        CreateConsentrationPoint();
        GenerateDestroyPoint();

    }

    private void Update()
    {
        
        if (_numberLevel == 3)
        {
            SwapParticles();
            _numberLevel++;
        }
        else if (_numberLevel == 6)
        {
            SwapParticles();
            _numberLevel++;
        }
    }

    private void SwapParticles()
    {
        if (_cosmos == true)
        {
           

            for (int i = 0; i < _particleEffectsConteiners[0].transform.childCount; i++)
            {
                int k = Random.Range(2, 4);
                _particleEffectsConteiners[0].transform.GetChild(i).GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, _particleSkins[k]);
            }
            for (int i = 0; i < _particleEffectsConteiners[1].transform.childCount; i++)
            {
                _particleEffectsConteiners[1].transform.GetChild(i).GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, _particleSkins[1]);
            }
        }
        else
        {
            for (int i = 0; i < _particleEffectsConteiners[0].transform.childCount; i++)
            {
                _particleEffectsConteiners[0].transform.GetChild(i).GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, _particleSkins[0]);
            }
            _cosmos = true;
        }
    }

    // ������ ������ ������
    public void NextLevel()
    {
        trix = Random.Range(1, 4);
        _numberLevel++;
        GenerateContainer();
        CreateConsentrationPoint();
        _player.GetComponent<PlayerMove>().Speed += 0.5f;
        StartCoroutine(ReloadDestroyPoint());
    }

    private IEnumerator ReloadDestroyPoint()
    {
        
        yield return new WaitForSeconds(5);
        GenerateDestroyPoint();
        StartCoroutine(ReloadDestroyPoint());
    }
    int trix = 1;
    private void CreateEnemies()
    {
        
        if (_numberLevel == 1)
        {
            trix = 1;
        }
        GenerateEnemies(trix);
        _countLines = trix;
        
    }

    private void GenerateEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = Instantiate(_linePrefab);
            prefab.GetComponent<SpriteRenderer>().color = _outColor[i];
            prefab.transform.position = new Vector3(_linePrefab.transform.position.x, _player.transform.position.y + i + 7, _linePrefab.transform.position.z);
        }
        
    }

    [SerializeField]
    private GameObject _colorsConteiner;

    private void GenerateContainer()
    {
        
        GameObject mainColorPref;
        GameObject colorPref;
        _colorsConteiner.transform.position = new Vector3(_colorsConteiner.transform.position.x, _player.transform.position.y + 3.12f, _colorsConteiner.transform.position.z);
        mainColorPref = _colorsConteiner.transform.GetChild(trix - 1).gameObject;
        mainColorPref.SetActive(true);
        for (int i = 0; i < trix; i++)
        {
            int k = Random.Range(0, 4);
            _outColor[i] =  _colorsLine[k];
            colorPref = mainColorPref.transform.GetChild(i).gameObject;
            colorPref.GetComponent<SpriteRenderer>().color = _outColor[i];
        }
        CreateEnemies();
        
    }

    private void CreateConsentrationPoint()
    {
        GameObject prefab = Instantiate(_consentrationPrefab);
        prefab.transform.position = new Vector3(_consentrationPrefab.transform.position.x, _player.transform.position.y + 4.12f, _consentrationPrefab.transform.position.z);

    }

    public void GenerateShild()
    {
        _uiManager.GetComponent<UIManagerGame>().PressInitDefenceBtn(_outColor, trix);
    }

    public void StartConsentration()
    {
        _uiManager.GetComponent<UIManagerGame>().Consentration();
    }

    [SerializeField]
    private Transform[] _destroyPoint;
    [SerializeField]
    private GameObject _destroyPrefab;
    [SerializeField]
    private Transform _destroyContainer;
    
    private void GenerateDestroyer()
    {
        GameObject prefab;
        for (int i = 0; i < 10; i++)
        {
            prefab = Instantiate(_destroyPrefab);
            prefab.gameObject.transform.SetParent(_destroyContainer);
        }
    }

    public void GenerateDestroyPoint()
    {
        
        for (int i = 0; i < trix + 4; i++)
        {
            CreateDestroyer();
        }
        
    }

    private void CreateDestroyer()
    {
        int i = Random.Range(1, 10);
        _destroyContainer.GetChild(i).transform.position = _destroyPoint[i].position;
    }
}
