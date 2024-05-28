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
        RollDiceState,
        MovePieceState,
        MovingPiece,
        EndTurn
    }

    public GameState currentState;

    private int currentPlayer = 0;
    private int numPlayers = 4;
    private int diceNumberRolled = 0;

    private string currentPlayerColor;
    private bool playerCanPlay;

    // Move Piece State stats
    float movePieceTimer;
    float movePieceTimerMax = .5f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentState = GameState.RollDiceState;
        StartTurn();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.RollDiceState:
                movePieceTimer = movePieceTimerMax;
                playerCanPlay = false;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RollDice();
                }
                break;

            case GameState.MovePieceState:
                if (!playerCanPlay)
                {
                    movePieceTimer -= Time.deltaTime;
                    if(movePieceTimer < 0)
                    {
                        EndTurn();
                        break;
                    }
                }
                break;

            case GameState.EndTurn:
                EndTurn();
                break;
        }
    }

    void StartTurn()
    {
        currentState = GameState.RollDiceState;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RollDice()
    {
        int minDieNumber = 1;
        int maxDieNumber = 7;

        diceNumberRolled = UnityEngine.Random.Range(minDieNumber, maxDieNumber);
        currentState = GameState.MovePieceState;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    void MovePiece()
    {
        currentState = GameState.EndTurn;
    }

    public void EndTurn()
    {
        if(diceNumberRolled != 6) currentPlayer = (currentPlayer + 1) % numPlayers;
        currentState = GameState.RollDiceState;
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
        return currentState == GameState.RollDiceState;
    }

    public bool IsMovePieceState()
    {
        return currentState == GameState.MovePieceState;
    }

    public int GetDiceNumberRolled()
    {
        return diceNumberRolled;
    }

    public void PlayerCanPlay()
    {
        playerCanPlay = true;
    }



}
