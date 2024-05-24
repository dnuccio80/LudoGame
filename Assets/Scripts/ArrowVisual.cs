using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;
using System;

public class ArrowVisual : MonoBehaviour
{
    private DiceContainer diceContainer;

    private void Awake()
    {
        diceContainer = GetComponentInParent<DiceContainer>();
    }

    void Start()
    {
        MoveArrow();
        GameManager.instance.OnRollDiceGameState += GameManager_OnRollDiceGameState;
        Hide();
    }

    private void GameManager_OnRollDiceGameState(object sender, EventArgs e)
    {
        if(diceContainer.GetPlayerColor() == GameManager.instance.GetCurrentPlayer())
        {
            Show();
        } else
        {
            Hide();
        }
    }

    public void MoveArrow()
    {
        float endMoveValue = 1.4f;
        float duration = .3f;

        transform.DOLocalMoveX(endMoveValue, duration)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
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
