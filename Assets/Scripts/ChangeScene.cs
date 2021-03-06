using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public void ChooseScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    [SerializeField]
    private GameObject _menuPanel;
    [SerializeField]
    private GameObject _gamePanel;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _prePlayer;
    [SerializeField]
    private GameObject _gm;
    [SerializeField]
    private GameObject _ui;
    [SerializeField]
    private ParticleSystem _startEffect;
    [SerializeField]
    private Animator _hideAnimatorSettings;
    [SerializeField]
    private AudioClip _startAudio;

    public void PressGameStart(GameObject currentObject)
    {
        currentObject.GetComponent<Button>().enabled = false;
        _startEffect.Play();
        HideSettings();
        
        StartCoroutine(EffectStart());
    }

    private void HideSettings()
    {
        _hideAnimatorSettings.SetBool("isHide", true);
    }

    private IEnumerator EffectStart()
    {
        _player.GetComponent<AudioSource>().PlayOneShot(_startAudio);
        yield return new WaitForSeconds(2);
        StartGame();
    }

    private void StartGame()
    {
        _player.SetActive(true);
        _prePlayer.SetActive(true);
        _gm.SetActive(true);
        _ui.SetActive(true);
        _menuPanel.SetActive(false);
        _gamePanel.SetActive(true);
    }
    public void WaitGame()
    {
        _prePlayer.SetActive(false);
        _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        _gm.SetActive(false);
    }
}
