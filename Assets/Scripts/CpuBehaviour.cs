using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuBehaviour : MonoBehaviour
{
    [SerializeField] private TokenScript[] tokenScriptArray;
    [SerializeField] private DiceLogic dice;

    private float timeToInvoke = .5f;

    List<TokenScript> tokensCanMoveList = new List<TokenScript>();
    List<TokenScript> tokensCanMoveToSecureZoneList = new List<TokenScript>();
    List<TokenScript> tokensCanEatList = new List<TokenScript>();
    List<TokenScript> tokensOutOfSecureZoneList = new List<TokenScript>();
    List<TokenScript> tokensToMoveToFinalWayList = new List<TokenScript>();
    List<TokenScript> tokensToMoveToGoalList = new List<TokenScript>();

    private PlayerSO playerSO;

    private bool canPlay;


    private void Start()
    {
        if (!canPlay)
        {
            Hide();
            return;
        }

        SetCpuPlayer();
        UpdateAllLists();
        GameManager.instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.instance.GetCurrentPlayer() != playerSO.ColorPlayer) return;

        if (GameManager.instance.IsRollDiceState()) Invoke("RollDice", timeToInvoke);
        else if (GameManager.instance.IsMovePieceState()) Invoke("ThinkMovement", timeToInvoke);

    }

    public void SetPlayerSO(PlayerSO _playerSO)
    {
        playerSO = _playerSO;
    }

    private void RollDice()
    {
        dice.RollDice();
    }

    public void CpuCanPlay()
    {
        canPlay = true;
    }

    public bool GetCpuCanPlay()
    {
        return canPlay;
    }

    private void ThinkMovement()
    {
        if (!GameManager.instance.GetCpuCanPlayMoreThanOneMovement()) return;

        int numberDiceRolled = GameManager.instance.GetDiceNumberRolled();

        UpdateAllLists();

        if (numberDiceRolled == 6)
        {
            bool canMoveOutOfHouse = false;

            foreach (TokenScript token in  tokenScriptArray)
            {
                if(!token.GetIfOutOfHouse())
                {
                    token.MoveOutOfHouse();
                    canMoveOutOfHouse = true;
                    break;
                } 
            }   
            if (!canMoveOutOfHouse)
            {
                MoveBestOption();
            }
        } else
        {
            MoveBestOption();
        }

    }

    private void UpdateTokensCanMoveList()
    {
        tokensCanMoveList.Clear();
        
        foreach(TokenScript token in tokenScriptArray)
        {
            if (!token.canMoveToken()) continue;
            tokensCanMoveList.Add(token);
        }

    }

    private void UpdateTokensCanMoveToSecureZoneList()
    {
        tokensCanMoveToSecureZoneList.Clear();

        foreach (TokenScript token in tokensCanMoveList)
        {
            if (token.HaveSecureZoneToMove())
            {
                tokensCanMoveToSecureZoneList.Add(token);
            }
        }
    }

    private void UpdatetokensCanEatList()
    {
        tokensCanEatList.Clear();

        foreach(TokenScript token in tokensCanMoveList)
        {
            if (token.HaveTokenToEat())
            {
                tokensCanEatList.Add(token);
            }
        }
    }

    private void UpdateTokensOutOfSecureZoneList()
    {
        tokensOutOfSecureZoneList.Clear();
        foreach (TokenScript token in tokensCanMoveList)
        {
            if (!token.IsInSecureZone())
            {
                tokensOutOfSecureZoneList.Add(token);
            }
        }
    }

    private void UpdatetokensToMoveToFinalWayList()
    {
        tokensToMoveToFinalWayList.Clear();
        foreach (TokenScript token in tokensCanMoveList)
        {
            if (token.CanMoveToFinalWay())
            {
                tokensToMoveToFinalWayList.Add(token);
            }
        }
    }

    private void UpdatetokensToMoveToGoalList()
    {
        tokensToMoveToGoalList.Clear();
        foreach (TokenScript token in tokensCanMoveList)
        {
            if (token.CanMoveToGoal())
            {
                tokensToMoveToGoalList.Add(token);
            }
        }
    }

    private void SetCpuPlayer()
    {
        GameManager.instance.SetCpuPlayer(playerSO);
    }

    private void UpdateAllLists()
    {
        UpdateTokensCanMoveList();
        UpdatetokensCanEatList();
        UpdateTokensCanMoveToSecureZoneList();
        UpdateTokensOutOfSecureZoneList();
        UpdatetokensToMoveToFinalWayList();
        UpdatetokensToMoveToGoalList();
    }

    private void MoveBestOption()
    {
        if (tokensCanEatList.Count > 0) EatToken();
        else if (tokensCanMoveToSecureZoneList.Count > 0) MoveToSecureZone();
        else if (tokensToMoveToGoalList.Count > 0) MoveToGoal();
        else if (tokensToMoveToFinalWayList.Count > 0) MoveToFinalWayZone();
        else if (tokensOutOfSecureZoneList.Count > 0) MoveTokenThatIsNotOnSecureZone();
        else MoveRandomToken();
    }

    private void EatToken()
    {
        tokensCanEatList[Random.Range(0, tokensCanEatList.Count)].TryMovePiece();
    }

    private void MoveToSecureZone()
    {
        tokensCanMoveToSecureZoneList[Random.Range(0, tokensCanMoveToSecureZoneList.Count)].TryMovePiece();
    }

    private void MoveToFinalWayZone()
    {
        tokensToMoveToFinalWayList[Random.Range(0, tokensToMoveToFinalWayList.Count)].TryMovePiece();
    }

    private void MoveToGoal()
    {
        tokensToMoveToGoalList[Random.Range(0, tokensToMoveToGoalList.Count)].TryMovePiece();
    }

    private void MoveTokenThatIsNotOnSecureZone()
    {
        tokensOutOfSecureZoneList[Random.Range(0, tokensOutOfSecureZoneList.Count)].TryMovePiece();
    }

    private void MoveRandomToken()
    {
        tokensCanMoveList[Random.Range(0, tokensCanMoveList.Count)].TryMovePiece();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
