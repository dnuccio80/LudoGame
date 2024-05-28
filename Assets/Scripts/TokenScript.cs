using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TokenScript : MonoBehaviour
{
    public event EventHandler OnStateChanged;
    
    [SerializeField] private Transform[] ways;
    [SerializeField] private PlayerSO playerSO;
    
    private int index = -1;
    private int movesNumber;
    private bool isOutHouse;

    private enum State
    {
        canMove,
        cannotMove
    }

    private State currentState;

    private void Start()
    {
        GameManager.instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        currentState = State.cannotMove;
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.instance.IsMovePieceState() && GameManager.instance.GetCurrentPlayer() == playerSO.ColorPlayer)
        {
            // It´s player turn & it is time to move any token
            transform.position = new Vector3(transform.position.x, transform.position.y, -.3f);

            if (!isOutHouse && GameManager.instance.GetDiceNumberRolled() != 6) return;

            int newPosition = index + GameManager.instance.GetDiceNumberRolled(); // Get if we can move between the "ways" array

            if (newPosition >= ways.Length) return; // Pass the ways Array so can not move the token

            GameManager.instance.PlayerCanPlay();
            currentState = State.canMove;
            OnStateChanged?.Invoke(this, EventArgs.Empty);

        } else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            currentState = State.cannotMove;
            OnStateChanged?.Invoke(this, EventArgs.Empty);

        }
    }

    public void TeyMovePiece()
    {
        if (currentState == State.cannotMove) return;

        if(isOutHouse) StartCoroutine(MovePieceRoutine());
        else
        {
            if (GameManager.instance.GetDiceNumberRolled() != 6) return; 
            MoveOutOfHouse();
        }

    }

    IEnumerator MovePieceRoutine()
    {
        float jumpPower = .4f;
        float duration = .3f;
        int numJumps = 1;

        GameManager.instance.MovingPiece();

        for (int i = 0; i < GameManager.instance.GetDiceNumberRolled(); i++)
        {
            index++;
            transform.DOLocalJump(ways[index].transform.position, jumpPower, numJumps, duration);
            yield return new WaitForSeconds(duration);
        }

        GameManager.instance.EndTurn();

    }

    private void MoveOutOfHouse()
    {
        isOutHouse = true;
        index++;
        float duration = .3f;
        GameManager.instance.MovingPiece();

        transform.DOLocalMove(ways[index].transform.position, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                GameManager.instance.EndTurn();
            });
    }

    public bool canMoveToken()
    {
        return currentState == State.canMove;
    }

    public string GetColorPlayer()
    {
        return playerSO.ColorPlayer;
    }

}
