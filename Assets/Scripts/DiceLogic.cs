using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceLogic : MonoBehaviour
{
    
    public void RollDie()
    {
        int minDieNumber = 1;
        int maxDieNumber = 7;

        int dieNumber = Random.Range(minDieNumber, maxDieNumber);

        Debug.Log(dieNumber);

        GameManager.instance.SetDieNumber(dieNumber);
    }






}
