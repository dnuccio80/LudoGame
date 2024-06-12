using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private PlayerSO playerSO;

    private int tokensOnGoal;

    public void SetPlayerSO(PlayerSO _playerSO)
    {
        playerSO = _playerSO;
    }

    public void TokenOnGoal()
    {
        tokensOnGoal++;

        if (tokensOnGoal == 4)
        {
            GameManager.instance.PlayerWin(playerSO);
            SoundManager.Instance.EmitWinningSound();
        }
        else
        {
            GameManager.instance.SamePlayerAgain();
            SoundManager.Instance.EmitGoalSound();
        }
    }


}
