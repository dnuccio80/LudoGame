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


    public event EventHandler OnRollDiceGameState;
    public event EventHandler OnMovePieceGameState;


    public enum GameState
    {
        RollDice,
        MovePiece,
        EndTurn
    }

    public GameState currentState;

    private int currentPlayer = 0;
    private int numPlayers = 4;
    private int diceRoll = 0;

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
                if (Input.GetMouseButtonDown(0))
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
        //Debug.Log("Turn Player: " + (currentPlayer + 1));
        currentState = GameState.RollDice;
        OnRollDiceGameState?.Invoke(this, EventArgs.Empty);
        GetCurrentPlayer();
    }

    void RollDice()
    {
        diceRoll = UnityEngine.Random.Range(1, 7); // Simula una tirada de dado de 6 caras
        //Debug.Log("Player " + (currentPlayer + 1) + " rolled the dice and got a " + diceRoll);
        currentState = GameState.MovePiece;
        OnMovePieceGameState?.Invoke(this, EventArgs.Empty);
    }

    void MovePiece()
    {
        // Aquí implementa la lógica para mover la ficha según el valor de diceRoll
        //Debug.Log("Player " + (currentPlayer + 1) + " move her piece " + diceRoll + " spaces");
        currentState = GameState.EndTurn;
    }

    void EndTurn()
    {
        currentPlayer = (currentPlayer + 1) % numPlayers;
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




}
