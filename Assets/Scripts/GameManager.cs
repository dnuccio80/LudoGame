using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance {  get; private set; }

    [SerializeField] private PlayerSO[] players;


    public event EventHandler OnGameStateChanged;

    public enum GameState
    {
        RollDice,
        MovePiece,
        MovingPiece,
        EndTurn
    }

    public GameState currentState;

    private int currentPlayer = 0;
    private int numPlayers = 4;
    private int diceNumberRolled = 0;

    private string currentPlayerColor;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentState = GameState.RollDice;
        StartTurn();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.RollDice:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RollDice();
                }
                break;

            case GameState.MovePiece:
                if (Input.GetMouseButtonDown(1))
                {
                    MovePiece();
                }
                break;

            case GameState.EndTurn:
                EndTurn();
                break;
        }
    }

    void StartTurn()
    {
        currentState = GameState.RollDice;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RollDice()
    {
        int minDieNumber = 1;
        int maxDieNumber = 7;

        diceNumberRolled = UnityEngine.Random.Range(minDieNumber, maxDieNumber);
        currentState = GameState.MovePiece;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    void MovePiece()
    {
        currentState = GameState.EndTurn;
    }

    public void EndTurn()
    {
        if(diceNumberRolled != 6) currentPlayer = (currentPlayer + 1) % numPlayers;
        currentState = GameState.RollDice;
        StartTurn();
    }

    public string GetCurrentPlayer()
    {
        switch(currentPlayer)
        {
            case 0:
                currentPlayerColor = players[0].ColorPlayer; break;
            case 1:
                currentPlayerColor = players[1].ColorPlayer; break;
            case 2:
                currentPlayerColor = players[2].ColorPlayer; break;
            case 3: 
                currentPlayerColor = players[3].ColorPlayer; break;
        }

        return currentPlayerColor;
    }

    public void MovingPiece()
    {
        currentState = GameState.MovingPiece;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsRollDiceState()
    {
        return currentState == GameState.RollDice;
    }

    public bool IsMovePieceState()
    {
        return currentState == GameState.MovePiece;
    }

    public int GetDiceNumberRolled()
    {
        return diceNumberRolled;
    }



}
