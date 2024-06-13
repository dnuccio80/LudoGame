using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private string playerName;
    
    private PlayerSO playerSO;

    private int tokensOnGoal = 3;

    public void SetPlayerSO(PlayerSO _playerSO)
    {
        playerSO = _playerSO;
    }

    public void TokenOnGoal()
    {
        tokensOnGoal++;

        if (tokensOnGoal == 4)
        {
            GameManager.instance.PlayerWin(playerSO, playerName);
            SoundManager.Instance.EmitWinningSound();
        }
        else
        {
            GameManager.instance.SamePlayerAgain();
            SoundManager.Instance.EmitGoalSound();
        }
    }


}
