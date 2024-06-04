using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;

    private int tokensOnGoal;

    public void TokenOnGoal()
    {
        tokensOnGoal++;

        if (tokensOnGoal == 4) GameManager.instance.PlayerWin(playerSO);
    }


}
