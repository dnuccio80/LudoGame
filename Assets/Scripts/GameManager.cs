using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    [SerializeField] private List<PlayerSO> playersList;
    
    private List<PlayerSO> CpuPlayers = new List<PlayerSO>();

    public event EventHandler OnGameStateChanged;
    public event EventHandler OnMoveAutomatically;
    public event EventHandler<OnPlayerWinEventArgs> OnPlayerWin;

    public class OnPlayerWinEventArgs : EventArgs
    {
        public PlayerSO playerSO;
        public int playerWinPosition;
    }

    public enum GameState
    {
        StartGame,
        RollDiceState,
        MovePieceState,
        MovingPiece,
        EndTurn
    }

    [Header("Token Script")]
    [SerializeField] private TokenScript[] playerTokenScriptsArray;
    [SerializeField] private TokenScript[] cpu1TokenScriptsArray;
    [SerializeField] private TokenScript[] cpu2TokenScriptsArray;
    [SerializeField] private TokenScript[] cpu3TokenScriptsArray;

    [Header("Dice Containers")]
    [SerializeField] private DiceContainer playerDiceContainer;
    [SerializeField] private DiceContainer cpu1DiceContainer;
    [SerializeField] private DiceContainer cpu2DiceContainer;
    [SerializeField] private DiceContainer cpu3DiceContainer;

    [Header("Cpu Brains")]
    [SerializeField] private CpuBehaviour cpu1CpuBehaviour;
    [SerializeField] private CpuBehaviour cpu2CpuBehaviour;
    [SerializeField] private CpuBehaviour cpu3CpuBehaviour;

    [Header("Goals")]
    [SerializeField] private Goal playerGoal;
    [SerializeField] private Goal cpu1Goal;
    [SerializeField] private Goal cpu2Goal;
    [SerializeField] private Goal cpu3Goal;


    public GameState currentState;

    private int currentPlayer;
    private int diceNumberRolled = 0;
    private int sixRolledQuantity;
    private int tokensCanMove;
    private int playerWinPosition;
    private string currentPlayerColor;
    private bool playerCanPlay;
    private bool canRollDice;


    // Timers
    float movePieceTimer;
    float movePieceTimerMax = .25f;
    float rollDiceTimer;
    float rollDiceTimerMax = .3f;
    float startGameTimer;
    float startGameTimerMax = 1f;

    private void Awake()
    {
        instance = this;
        SetVisual();
    }

    private void Start()
    {
        rollDiceTimer = rollDiceTimerMax;
        startGameTimer = startGameTimerMax;
        currentPlayer = UnityEngine.Random.Range(0, playersList.Count);
        StartGame();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.StartGame:
                startGameTimer -= Time.deltaTime; 
                
                if(startGameTimer < 0)
                {
                    StartTurn();
                    startGameTimer = startGameTimerMax;
                }
                break;

            case GameState.RollDiceState:

                movePieceTimer = movePieceTimerMax;
                tokensCanMove = 0;
                playerCanPlay = false;

                rollDiceTimer -= Time.deltaTime;

                if (rollDiceTimer <= 0)
                {
                    canRollDice = true;
                }

                break;

            case GameState.MovePieceState:
                rollDiceTimer = rollDiceTimerMax;
                canRollDice = false;
                if (!playerCanPlay)
                {
                    movePieceTimer -= Time.deltaTime;
                    if (movePieceTimer < 0)
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
        }
    }

    private void SetVisual()
    {

        int index = 0;


        foreach (TokenScript token in playerTokenScriptsArray)
        {
            token.SetPlayerSO(PlayerStats.GetPlayerSO());
            playerDiceContainer.SetPlayerSO(PlayerStats.GetPlayerSO());
            playerGoal.SetPlayerSO(PlayerStats.GetPlayerSO());
        }

        for (int i = 0; i < playersList.Count; i++)
        {
            if (playersList[i].ColorPlayer == PlayerStats.GetPlayerColor())
            {
                index = i;
                break;
            }
        }

        if (index < playersList.Count - 1) index++;
        else index = 0;

        foreach (TokenScript token in cpu1TokenScriptsArray)
        {
            token.SetPlayerSO(playersList[index]);
        }

        cpu1DiceContainer.SetPlayerSO(playersList[index]);
        cpu1CpuBehaviour.SetPlayerSO(playersList[index]);
        cpu1Goal.SetPlayerSO(playersList[index]);


        if (index < playersList.Count - 1) index++;
        else index = 0;

        foreach (TokenScript token in cpu2TokenScriptsArray)
        {
            token.SetPlayerSO(playersList[index]);
        }

        cpu2DiceContainer.SetPlayerSO(playersList[index]);
        cpu2CpuBehaviour.SetPlayerSO(playersList[index]);
        cpu2Goal.SetPlayerSO(playersList[index]);


        if (index < playersList.Count - 1) index++;
        else index = 0;

        foreach (TokenScript token in cpu3TokenScriptsArray)
        {
            token.SetPlayerSO(playersList[index]);
        }

        cpu3DiceContainer.SetPlayerSO(playersList[index]);
        cpu3CpuBehaviour.SetPlayerSO(playersList[index]);
        cpu3Goal.SetPlayerSO(playersList[index]);

    }


    void StartGame()
    {
        currentState = GameState.StartGame;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    void StartTurn()
    {
        currentState = GameState.RollDiceState;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RollDice()
    {
        if (!canRollDice) return;

        SoundManager.Instance.EmitRollDiceSound();

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

        Invoke("MovePieceState", .3f);
    } 

    private void MovePieceState()
    {
        currentState = GameState.MovePieceState;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void EndTurn()
    {
        if(diceNumberRolled != 6 || sixRolledQuantity == 3 || !playerCanPlay)
        {
            currentPlayer = (currentPlayer + 1) % playersList.Count;
            sixRolledQuantity = 0;
        }

        StartTurn();
    }

    public void SamePlayerAgain()
    {
        sixRolledQuantity = 0;
        SoundManager.Instance.EmitStartTurnSound();
        StartTurn();
    }

    public string GetCurrentPlayer()
    {
        currentPlayerColor = playersList[currentPlayer].ColorPlayer;

        return currentPlayerColor;
    }

    public void SetCpuPlayer(PlayerSO playerSO)
    {
        for(int i = 0; i < playersList.Count; i++)
        {
            if (playersList[i].ColorPlayer == playerSO.ColorPlayer)
            {
                CpuPlayers.Add(playersList[i]);
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

    public bool IsStartGameState()
    {
        return currentState == GameState.StartGame;
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
        foreach(PlayerSO player in playersList)
        {
            if(playerSO.ColorPlayer == player.ColorPlayer)
            {
                playersList.Remove(player);
                break;
            }
        }

        EndTurn();

        playerWinPosition++;

        OnPlayerWin?.Invoke(this, new OnPlayerWinEventArgs
        {
            playerSO = playerSO,
            playerWinPosition = playerWinPosition
        });
    }

    public bool GetCpuCanPlayMoreThanOneMovement()
    {
        // this void is for CPU thinking, so we need to get if player can execute more than one token movement.
        return (playerCanPlay && tokensCanMove > 1);
    }

    public bool GetCanRollDice()
    {
        return canRollDice;
    }


}
