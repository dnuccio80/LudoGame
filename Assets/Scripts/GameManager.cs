using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    [SerializeField] private List<PlayerSO> playersList;

    private List<PlayerSO> CpuPlayers = new List<PlayerSO>();
    private List<string> finishPositionsList = new List<string>();
    private List<Goal> goalList = new List<Goal>();

    public event EventHandler OnGameStateChanged;
    public event EventHandler OnMoveAutomatically;
    public event EventHandler<OnGameFinishedEventArgs> OnGameFinished;
    public event EventHandler<OnPlayerWinEventArgs> OnPlayerWin;

    public class OnPlayerWinEventArgs : EventArgs
    {
        public PlayerSO playerSO;
        public int playerWinPosition;
    }

    public class OnGameFinishedEventArgs : EventArgs
    {
        public List<string> winList;
    }

    public enum GameState
    {
        StartGame,
        RollDiceState,
        MovePieceState,
        MovingPiece,
        EndTurn,
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

    [Header("Win Crowns UI")]
    [SerializeField] private WinCrown playerWinCrown;
    [SerializeField] private WinCrown cpu1WinCrown;
    [SerializeField] private WinCrown cpu2WinCrown;
    [SerializeField] private WinCrown cpu3WinCrown;


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
            playerWinCrown.SetPlayerSO(PlayerStats.GetPlayerSO());
            playerDiceContainer.SetCanPlay();
            token.SetCanPlay();
        }

        for (int i = 0; i < playersList.Count; i++)
        {
            if (playersList[i].ColorPlayer == PlayerStats.GetPlayerColor())
            {
                index = i;
                break;
            }
        }

        if (PlayerStats.GetNumberPlayers() == 2)
        {

            index = (index + 2) % playersList.Count;

            PlayerSO CpuPlayerSO = playersList[index];

            playersList.Clear();

            playersList.Add(PlayerStats.GetPlayerSO());
            playersList.Add(CpuPlayerSO);

            foreach(TokenScript token in cpu2TokenScriptsArray)
            {
                token.SetPlayerSO(CpuPlayerSO);
                token.SetCanPlay();
            }

            SetNewCpuPlayer(CpuPlayerSO, cpu2DiceContainer, cpu2CpuBehaviour, cpu2Goal, cpu2WinCrown);


        }
        else if(PlayerStats.GetNumberPlayers() == 4)
        {
            index = (index + 1) % playersList.Count;

            foreach (TokenScript token in cpu1TokenScriptsArray)
            {
                token.SetPlayerSO(playersList[index]);
                token.SetCanPlay();

            }

            SetNewCpuPlayer(playersList[index], cpu1DiceContainer, cpu1CpuBehaviour, cpu1Goal, cpu1WinCrown);


            index = (index + 1) % playersList.Count;

            foreach (TokenScript token in cpu2TokenScriptsArray)
            {
                token.SetPlayerSO(playersList[index]);
                token.SetCanPlay();

            }

            SetNewCpuPlayer(playersList[index], cpu2DiceContainer, cpu2CpuBehaviour, cpu2Goal, cpu2WinCrown);


            index = (index + 1) % playersList.Count;

            foreach (TokenScript token in cpu3TokenScriptsArray)
            {
                token.SetPlayerSO(playersList[index]);
                token.SetCanPlay();

            }

            SetNewCpuPlayer(playersList[index], cpu3DiceContainer, cpu3CpuBehaviour, cpu3Goal,cpu3WinCrown);
        }

    }

    public void UpdateGoalList(Goal goal)
    {
        if(!goalList.Contains(goal)) goalList.Add(goal);
    }

    private void SetNewCpuPlayer(PlayerSO playerSO, DiceContainer cpuDiceContainer, CpuBehaviour cpuBehaviour, Goal cpuGoal, WinCrown winCrown)
    {
        cpuDiceContainer.SetCanPlay();
        cpuBehaviour.CpuCanPlay();
        cpuDiceContainer.SetPlayerSO(playerSO);
        cpuBehaviour.SetPlayerSO(playerSO);
        cpuGoal.SetPlayerSO(playerSO);
        winCrown.SetPlayerSO(playerSO);
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

    public void PlayerWin(PlayerSO playerSO, string playerName)
    {
        foreach(PlayerSO player in playersList)
        {
            if(playerSO.ColorPlayer == player.ColorPlayer)
            {
                playersList.Remove(player);
                break;
            }
        }

        finishPositionsList.Add(playerName);
        playerWinPosition++;

        int playersFinished = finishPositionsList.Count;

        if (PlayerStats.GetNumberPlayers() == 2 && playersFinished == 1) GameFinished();
        else if (PlayerStats.GetNumberPlayers() == 4 && playersFinished == 3) GameFinished();
        else
        {
            EndTurn();


            OnPlayerWin?.Invoke(this, new OnPlayerWinEventArgs
            {
                playerSO = playerSO,
                playerWinPosition = playerWinPosition
            });
        }
    }

    private void GameFinished()
    {
        foreach(Goal goal in goalList)
        {
            if (!finishPositionsList.Contains(goal.GetPlayerName())) finishPositionsList.Add(goal.GetPlayerName());
        }

        OnGameFinished?.Invoke(this, new OnGameFinishedEventArgs
        {
            winList = finishPositionsList
        });
    }

    public int GetPlayerFinishPosition()
    {
        int playerFinishPosition = 0;

        for(int i = 0; i < finishPositionsList.Count; i++)
        {
            if (finishPositionsList[i] == playerGoal.GetPlayerName())
            {
                playerFinishPosition = i + 1;
            }
        }

        return playerFinishPosition;
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
