using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTurns : MonoBehaviour
{
    private enum State
    {
        WaitingForPlayer,
        movingPlayer,
        WaitingForComputer1,
        movingComputer1,
        WaitingForComputer2,
        movingComputer2,
        WaitingForComputer3,
        movingComputer3,
    }

    private State currentState;

    private void Update()
    {
        if(currentState == State.WaitingForPlayer)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                int randomDieNumber = Random.Range(1,7);

                Debug.Log("Tirando el numbero " +  randomDieNumber);
            }
        }
    }





}
