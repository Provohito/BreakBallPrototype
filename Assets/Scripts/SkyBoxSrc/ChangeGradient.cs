using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ChangeGradient : MonoBehaviour
{
    [SerializeField]
    private Material _mat;
    [SerializeField]
    private Gradient _topGradient;
    [SerializeField]
    private Gradient _lowGradient;
    [SerializeField, Range(0f, 1f)] private float timeProgress;
    [SerializeField, Range(1f, 3600f)] private float timeDalay = 60;
    private Color _topColor;
    [SerializeField]
    private Text _text;
    void Start()
    {
        
    }

    
    void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        _text.text = Convert.ToInt64(fps).ToString() + " FPS";
        if (Application.isPlaying)
            timeProgress += Time.deltaTime / timeDalay;
        

        if (timeProgress > 1f)
            timeProgress = 0f;

        _mat.SetColor("_TopColor", _topGradient.Evaluate(timeProgress));
        _mat.SetColor("_BottomColor", _lowGradient.Evaluate(timeProgress));
    }
}
