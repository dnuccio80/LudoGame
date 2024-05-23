using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance {  get; private set; }

    public event EventHandler OnStateChanged;

    private int randomDiceNumber;

    private enum GameState
    {
        TurnForRed,
        TurnForGreen,
        TurnForYellow,
        TurnForLightBlue
    }

    private GameState currentState;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeState();
        }

        if(Input.GetKeyUp(KeyCode.C))
        {
            randomDiceNumber = UnityEngine.Random.Range(1, 7);
        }
    }

    private void ChangeState()
    {
        currentState++;
        int totalStates = GameState.GetValues(typeof(GameState)).Length;
        int gameStateIndex = GetEnumIndex(currentState);

        if (gameStateIndex == -1) currentState = GetFirstEnumState<GameState>();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private int GetEnumIndex(GameState state)
    {
        GameState[] enumValues = (GameState[])Enum.GetValues(typeof(GameState));

        int index = Array.IndexOf(enumValues, state);

        return index;
    }

    T GetFirstEnumState<T>() where T : Enum
    {
        // Get the values of the enum as an array
        T[] enumValues = (T[])Enum.GetValues(typeof(T));

        // Return the first value in the array
        return enumValues[0];
    }

    public bool IsYellowTurn()
    {
        return currentState == GameState.TurnForYellow;
    }

    public bool IsGreenTurn()
    {
        return currentState == GameState.TurnForGreen;
    }

    public bool IsRedTurn()
    {
        return currentState == GameState.TurnForRed;
    }

    public bool IsLightBlueTurn()
    {
        return currentState == GameState.TurnForLightBlue;
    }

    public int GetDieNumber()
    {
        return randomDiceNumber;
    }

}
