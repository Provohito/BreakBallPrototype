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
    private bool _isTrueWayToGrad = true;
    void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        _text.text = Convert.ToInt64(fps).ToString() + " FPS";
        if (Application.isPlaying)
        {
            if (_isTrueWayToGrad)
            {
                if (timeProgress <= 1)
                    timeProgress += Time.deltaTime / timeDalay;
                else
                    _isTrueWayToGrad = false;
            }
            else
            {
                if (timeProgress >= 0)
                    timeProgress -= Time.deltaTime / timeDalay;
                else
                    _isTrueWayToGrad = true;
            }
        }

        _mat.SetColor("_TopColor", _topGradient.Evaluate(timeProgress));
        _mat.SetColor("_BottomColor", _lowGradient.Evaluate(timeProgress));
    }
}
