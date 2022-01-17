using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void PressGameStart()
    {
        _startEffect.Play();
        StartCoroutine(EffectStart());
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
