using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class DiceContainer : MonoBehaviour
{

    public event EventHandler OnDiceStateChanged;
    
    private PlayerSO playerSO;


    private enum DiceState
    {
        CanBeRolled,
        AlreadyRolled
    }

    private DiceState currentDiceState;

    private void Start()
    {
        currentDiceState = DiceState.CanBeRolled;
        GameManager.instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e)
    {
        if (GameManager.instance.IsRollDiceState()) ResetDice();
        else if (GameManager.instance.IsMovePieceState()) DiceRolled();
    }

    public void SetPlayerSO(PlayerSO _playerSO)
    {
        playerSO = _playerSO;
    }

    public PlayerSO GetPlayerSO() { return playerSO; }

    public string GetPlayerColor()
    {
        return playerSO.ColorPlayer;
    }
    
    public void DiceRolled()
    {
        currentDiceState = DiceState.AlreadyRolled;
        OnDiceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ResetDice()
    {
        currentDiceState = DiceState.CanBeRolled;
        OnDiceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanDiceBeRolled()
    {
        return currentDiceState == DiceState.CanBeRolled;
    }



}
