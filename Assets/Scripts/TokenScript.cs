using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TokenScript : MonoBehaviour
{

    [SerializeField] private Transform[] ways;
    [SerializeField] private PlayerSO playerSO;

    private int index = -1;
    private bool canMove = false;
    private int movesNumber;
    private bool isOutHouse;

    private void Start()
    {
        
    }

    public void MovePiece()
    {
        if(isOutHouse) StartCoroutine(MovePieceRoutine());
        else
        {
            if (movesNumber != 6) return; 
            MoveOutOfHouse();
        } 
    }

    IEnumerator MovePieceRoutine()
    {
        float jumpPower = .4f;
        float duration = .3f;
        int numJumps = 1;

        for (int i = 0; i < movesNumber; i++)
        {
            index++;
            transform.DOLocalJump(ways[index].transform.position, jumpPower, numJumps, duration);
            yield return new WaitForSeconds(duration);
        }

    }

    private void MoveOutOfHouse()
    {
        isOutHouse = true;
        index++;
        float duration = .3f;
        transform.DOLocalMove(ways[index].transform.position, duration)
            .SetEase(Ease.Linear);
    }

}
