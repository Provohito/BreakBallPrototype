using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Color[] _colorsLine;

    private Color[] _outColor = new Color[5];
    private int _countLines;

    private int _numberLevel;
    public int NumberLevel { set {_numberLevel = value; } get {return _numberLevel; } }

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

    [SerializeField]
    private Sprite[] _newSkinsActive;
    [SerializeField]
    private GameObject _newSkinPrefab;
    [SerializeField]
    private UnlockSkinsSystem _unSys;

    private void Start()
    {
        _numberLevel = 1;
        GenerateContainer();
        GenerateDestroyer();
    }

    [System.Obsolete]
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

    [System.Obsolete]
    private void SwapParticles()
    {
        if (_cosmos == true)
        {
           

            for (int i = 0; i < _particleEffectsConteiners[0].transform.childCount; i++)
            {
                int k = Random.Range(2, 4);
                SetParticle(_particleEffectsConteiners[0], _particleSkins[k], i);
                //_particleEffectsConteiners[0].transform.GetChild(i).GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, _particleSkins[k]);
            }
            for (int i = 0; i < _particleEffectsConteiners[1].transform.childCount; i++)
            {
                SetParticle(_particleEffectsConteiners[1], _particleSkins[1], i);
                //_particleEffectsConteiners[1].transform.GetChild(i).GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, _particleSkins[1]);
            }
        }
        else
        {
            for (int i = 0; i < _particleEffectsConteiners[0].transform.childCount; i++)
            {
                SetParticle(_particleEffectsConteiners[0], _particleSkins[0], i);
                //_particleEffectsConteiners[0].transform.GetChild(i).GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, _particleSkins[0]);
            }
            _cosmos = true;
        }
    }

    private void SetParticle(GameObject conteiner, Sprite sprite, int index)
    {
        StartCoroutine(UnVisibleSprite(index, conteiner, sprite));
        
    }


    private IEnumerator VisibleSprite(int index, GameObject conteiner)
    {
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            conteiner.transform.GetChild(index).localScale += new Vector3(1.1f, 1.1f, 1.1f);
            yield return new WaitForSeconds(0.05f);
        }
    }
    private IEnumerator UnVisibleSprite(int index, GameObject conteiner, Sprite sprite)
    {
        for (float f = 1f; f >= 0; f -= 0.05f)
        {
            conteiner.transform.GetChild(index).localScale -= new Vector3(1.1f,1.1f,1.1f);
            
            yield return new WaitForSeconds(0.05f);
        }
        conteiner.transform.GetChild(index).GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, sprite);
        StartCoroutine(VisibleSprite(index, conteiner));
        
        
    }
    public int CountLines { set { countLines = value; } get { return countLines; } }
    private int countLines = 1;

    public void NextLevel()
    {
        Debug.Log(countLines);
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < _enemies.Length; i++)
        {

            Destroy(_enemies[i]);
        }
        NumberLevel++;
        for (int i = 0; i < _colorsConteiner.transform.childCount; i++)
        {
            _colorsConteiner.transform.GetChild(i).gameObject.SetActive(false);
        }
        // Сделать рассчет
        if (countLines > 4)
        {
            trix = Random.Range(1, 4);
        }
        else
            trix = countLines;
        
        
        if (_numberLevel == 2)
        {
            StartCoroutine(ReloadDestroyPoint());
        }
        else if (_numberLevel > 5)
        {
            StartCoroutine(ReloadDestroyPoint());
        }
        GenerateContainer();
        
        _player.GetComponent<PlayerMove>().Speed += 0.03f; // Увеличение скорости
        
        
    }
    [SerializeField]
    private AudioClip _newSkinSound;
    [SerializeField]
    private GameObject _newSkinConteiner;
    private IEnumerator ReloadDestroyPoint()
    {
        yield return new WaitForSeconds(9);
        GenerateDestroyPoint();
        int k = Random.Range(1, 100);
        int i = Random.Range(0,15);
        if (k == 27)
        {
            
            
            _unSys.CheckSkin(_newSkinsActive[i]);
            Debug.Log(_unSys.StateSkin);
            if ( _unSys.StateSkin == true)
            {
                _player.GetComponent<AudioSource>().PlayOneShot(_newSkinSound);
                
                GameObject newskin = Instantiate(_newSkinPrefab);
                
                newskin.transform.position = new Vector3(newskin.transform.position.x, _player.transform.position.y + 12, newskin.transform.position.z);
                newskin.GetComponent<SpriteRenderer>().sprite = _newSkinsActive[i];
                _newSkinConteiner.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = _newSkinsActive[i];
                _newSkinConteiner.GetComponent<Animator>().Play("OpenCloseAnimation");
                _unSys.Save(_newSkinsActive[i]);
                

            }
        }
        StartCoroutine(ReloadDestroyPoint());
    }
    private int trix = 1;

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
            prefab.transform.position = new Vector3(_linePrefab.transform.position.x, _player.transform.position.y + i + 9, _linePrefab.transform.position.z);
        }
        
    }

    [SerializeField]
    private GameObject _colorsConteiner;

    private void GenerateContainer()
    {
        
        GameObject mainColorPref;
        GameObject colorPref;
        _colorsConteiner.transform.position = new Vector3(_colorsConteiner.transform.position.x, _player.transform.position.y + 7.12f, _colorsConteiner.transform.position.z);
        mainColorPref = _colorsConteiner.transform.GetChild(trix - 1).gameObject;
        mainColorPref.transform.position = new Vector3(_colorsConteiner.transform.position.x + (float)Random.Range(-2, 2), _player.transform.position.y + 7.12f, _colorsConteiner.transform.position.z);
        mainColorPref.SetActive(true);
        for (int i = 0; i < trix; i++)
        {
            int k = Random.Range(0, 5);
            _outColor[i] =  _colorsLine[k];
            colorPref = mainColorPref.transform.GetChild(0).transform.GetChild(i).gameObject;
            colorPref.GetComponent<SpriteRenderer>().color = _outColor[i];
        }
        CreateEnemies();
        
    }

    public void GenerateShild()
    {
        Debug.Log(trix);
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

    private GameObject prefab;
    private void GenerateDestroyer()
    {
        
        for (int i = 0; i < 10; i++)
        {
            prefab = Instantiate(_destroyPrefab);
            prefab.gameObject.transform.SetParent(_destroyContainer);
            prefab.gameObject.SetActive(false);
            prefab.transform.position = new Vector3(-6,1,0);
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
        if (!_destroyContainer.GetChild(i).gameObject.activeInHierarchy)
        {
            _destroyContainer.GetChild(i).transform.position = _destroyPoint[i].position;
            _destroyContainer.GetChild(i).gameObject.SetActive(true);
        }  
    }
}
