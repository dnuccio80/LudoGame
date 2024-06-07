using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Search;
using UnityEngine;

public class DiceLogic : MonoBehaviour
{
    private DiceContainer diceContainer;
    [SerializeField] private Sprite[] diceNumbersSpriteArray;

    private void Awake()
    {
        diceContainer = GetComponentInParent<DiceContainer>();
    }

    private void Start()
    {
        GameManager.instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        Hide();
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        (diceContainer.GetPlayerColor() == GameManager.instance.GetCurrentPlayer() ? (Action)Show : Hide)();

        if (GameManager.instance.IsMovePieceState()) GetComponent<SpriteRenderer>().sprite = diceNumbersSpriteArray[GameManager.instance.GetDiceNumberRolled() - 1];
    }

    public void RollDice()
    {
        if (!diceContainer.CanDiceBeRolled()) return;
        if (!GameManager.instance.GetCanRollDice()) return;

        GameManager.instance.RollDice();
        diceContainer.DiceRolled();
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
