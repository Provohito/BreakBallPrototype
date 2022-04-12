using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Application.isPlaying)
            timeProgress += Time.deltaTime / timeDalay;
        

        if (timeProgress > 1f)
            timeProgress = 0f;

        _mat.SetColor("_TopColor", _topGradient.Evaluate(timeProgress));
        _mat.SetColor("_BottomColor", _lowGradient.Evaluate(timeProgress));
    }
}
