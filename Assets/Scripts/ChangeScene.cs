using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void ChooseScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
