using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceSelectBottanEffect : MonoBehaviour
{
    Color _color;
    Tween _tween;

    private void Start()
    {
        _color = this.GetComponent<Image>().color;
    }

    public void enterDistanceEffect()
    {
        _tween =  this.GetComponent<Image>().DOColor(new Color(1, 1, 1), 0.3f);
    }

    public void exitDistanceEffect()
    {
        _tween.Kill();
        this.GetComponent<Image>().color = _color;
    }
}
