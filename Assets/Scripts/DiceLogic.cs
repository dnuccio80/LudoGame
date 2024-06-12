using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Search;
using UnityEngine;

public class DiceLogic : MonoBehaviour
{
    private const string ROLL_ANIMATION = "Roll";

    private DiceContainer diceContainer;
    private Animator animator;
    [SerializeField] private Sprite[] diceNumbersSpriteArray;

    private void Awake()
    {
        diceContainer = GetComponentInParent<DiceContainer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if(!diceContainer.GetCanPlay())
        {
            Hide();
            return;
        }
        GameManager.instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        Hide();
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        (diceContainer.GetPlayerColor() == GameManager.instance.GetCurrentPlayer() ? (Action)Show : Hide)();

    }

    public void RollDice()
    {
        if (!diceContainer.CanDiceBeRolled()) return;
        if (!GameManager.instance.GetCanRollDice()) return;

        GameManager.instance.RollDice();
        animator.SetTrigger(ROLL_ANIMATION);
        Invoke("SetNumberRolled", .14f);
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

    public void SetNumberRolled()
    {
        GetComponent<SpriteRenderer>().sprite = diceNumbersSpriteArray[GameManager.instance.GetDiceNumberRolled() - 1];
    }

}
