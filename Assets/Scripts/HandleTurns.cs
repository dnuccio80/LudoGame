using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class HandleTurns : MonoBehaviour
{

    public static HandleTurns instance {  get; private set; }

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
       public GameState state;
    }


    public enum GameState
    {
        WaitingForRed,
        WaitingForGreen,
        WaitingForLightBlue,
        WaitingForYellow
    }

    public GameState currentState;

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
    }

    private void ChangeState()
    {
        currentState++;
        int totalStates = GameState.GetValues(typeof(GameState)).Length;
        int gameStateIndex = GetEnumIndex(currentState);

        if (gameStateIndex == -1) currentState = GetFirstEnumState<GameState>();
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = currentState
        }) ;
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

    public bool IsGreenTurn()
    {
        return currentState == GameState.WaitingForGreen;
    }

}
