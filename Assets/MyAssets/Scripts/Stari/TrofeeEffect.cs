using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrofeeEffect : MonoBehaviour
{
    Vector3 _firstAngle;
    Vector3 _firstSize;

    Sequence sequence;

    private void Start()
    {
        _firstAngle = transform.eulerAngles;
        _firstSize = transform.localScale;
    }

    public void _enterTrophy()
    {
        sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(new Vector3(0.09f, 0.09f, 0.09f), 0.8f).SetEase(Ease.InBack));
        //0.09
        sequence.Append(transform.DORotate(new Vector3(0, 0, 13f), 0.3f));
        sequence.Append(transform.DORotate(new Vector3(0, 0, -13f), 0.6f));
        sequence.Append(transform.DORotate(new Vector3(0, 0, 13f), 0.6f));
        sequence.Append(transform.DORotate(new Vector3(0, 0, 0), 0.3f));


        sequence.Play();
    }
    public void _exitTrophy() 
    {
        sequence.Kill();
        this.gameObject.transform.eulerAngles = _firstAngle;
        this.gameObject.transform.localScale = _firstSize;
    }
}
