using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowVisual : MonoBehaviour
{

    private float endMoveValue = 1.4f;
    private float duration = .3f;

    void Start()
    {
        transform.DOLocalMoveX(endMoveValue, duration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }

}
