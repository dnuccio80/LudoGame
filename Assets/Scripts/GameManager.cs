using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance {  get; private set; }

    [SerializeField] private List<PlayerSO> players;

    private List<PlayerSO> CpuPlayers = new List<PlayerSO>();

    public event EventHandler OnGameStateChanged;
    public event EventHandler OnMoveAutomatically;

    public enum GameState
    {
        RollDiceState,
        MovePieceState,
        MovingPiece,
        EndTurn
    }

    public GameState currentState;

    private int currentPlayer = 0;
    private int diceNumberRolled = 0;
    private int sixRolledQuantity;
    private int tokensCanMove;
    private string currentPlayerColor;
    private bool playerCanPlay;

    // Move Piece State stats
    float movePieceTimer;
    float movePieceTimerMax = .25f;

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
                tokensCanMove = 0;
                playerCanPlay = false;
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
                } else
                {
                    if (tokensCanMove > 1) return;

                    movePieceTimer -= Time.deltaTime;
                    if (movePieceTimer < 0)
                    {
                        if (tokensCanMove == 1)
                        {
                            OnMoveAutomatically?.Invoke(this, EventArgs.Empty);
                        }
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
        if (diceNumberRolled == 6)
        {
            sixRolledQuantity++;
            if (sixRolledQuantity == 3)
            {
                EndTurn();
                return;
            }

        }
        currentState = GameState.MovePieceState;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    void MovePiece()
    {
        currentState = GameState.EndTurn;
    }

    public void EndTurn()
    {
        if(diceNumberRolled != 6 || sixRolledQuantity == 3 || !playerCanPlay)
        {
            currentPlayer = (currentPlayer + 1) % players.Count;
            sixRolledQuantity = 0;
        }

        currentState = GameState.RollDiceState;
        StartTurn();
    }

    public void SamePlayerAgain()
    {
        sixRolledQuantity = 0;
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

    public void SetCpuPlayer(PlayerSO playerSO)
    {
        for(int i = 0; i < players.Count; i++)
        {
            if (players[i].ColorPlayer == playerSO.ColorPlayer)
            {
                CpuPlayers.Add(players[i]);
            }
        }
    }

    public bool GetCurrentPlayerIsCpu()
    {
        foreach(PlayerSO player in CpuPlayers)
        {
            if(player.ColorPlayer == GetCurrentPlayer())
            {
                return true;
            }
        }

        return false;
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
        tokensCanMove++;
    }

    public void PlayerWin(PlayerSO playerSO)
    {
        foreach(PlayerSO player in players)
        {
            if(playerSO.ColorPlayer == player.ColorPlayer)
            {
                players.Remove(player);
                break;
            }
        }
    }

    public bool GetCpuCanPlayMoreThanOneMovement()
    {
        // this void is for CPU thinking, so we need to get if player can execute more than one token movement.
        return (playerCanPlay && tokensCanMove > 1);
    }


}
