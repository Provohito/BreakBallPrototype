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
        yield return new WaitForSeconds(3);
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
}
