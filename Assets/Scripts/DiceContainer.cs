using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class DiceContainer : MonoBehaviour
{

    private enum ColorPlayer
    {
        Green,
        LightBlue,
        Red,
        Yellow
    }

    public event EventHandler OnPlayerTurnChange;
    private bool isPlayerTurn; 

    [SerializeField] private ColorPlayer colorPlayer;

    private void Start()
    {
        GameManager.instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
       switch(colorPlayer)
        {
            case ColorPlayer.Green:
                isPlayerTurn = GameManager.instance.IsGreenTurn();
                break;
            case ColorPlayer.LightBlue:
                isPlayerTurn = GameManager.instance.IsLightBlueTurn();
                break;
            case ColorPlayer.Red:
                isPlayerTurn = GameManager.instance.IsRedTurn();
                break;
            case ColorPlayer.Yellow:
                isPlayerTurn = GameManager.instance.IsYellowTurn();
                break;
        }

        OnPlayerTurnChange?.Invoke(this, EventArgs.Empty);
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
