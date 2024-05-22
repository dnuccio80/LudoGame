using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;
using System;

public class ArrowVisual : MonoBehaviour
{

    private float endMoveValue = 1.4f;
    private float duration = .3f;

    private DiceContainer diceContainer;

    private void Awake()
    {
        diceContainer = GetComponentInParent<DiceContainer>();
    }

    void Start()
    {
        transform.DOLocalMoveX(endMoveValue, duration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);

        diceContainer.OnPlayerTurnChange += DiceContainer_OnPlayerTurn;
    }

    private void DiceContainer_OnPlayerTurn(object sender, System.EventArgs e)
    {
        (diceContainer.IsPlayerTurn() ? (Action)Show : Hide)();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
