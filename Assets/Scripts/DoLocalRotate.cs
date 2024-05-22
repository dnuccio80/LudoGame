using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoLocalRotate : MonoBehaviour
{
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 360f), .5f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

}
