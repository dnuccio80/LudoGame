using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTurns : MonoBehaviour
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs
    {
        private State state;
    }

    private enum State
    {
        WaitingForRed,
        WaitingForGreen,
        WaitingForLightBlue,
        WaitingForYellow
    }

    private State currentState;

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
        int totalStates = State.GetValues(typeof(State)).Length;
        int gameStateIndex = GetEnumIndex(currentState);

        if (gameStateIndex == -1) currentState = GetFirstEnumState<State>();
        Debug.Log(currentState);
    }

    private int GetEnumIndex(State state)
    {
        State[] enumValues = (State[])Enum.GetValues(typeof(State));

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


}
