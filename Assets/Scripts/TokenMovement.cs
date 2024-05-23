using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TokenMovement : MonoBehaviour
{

    [SerializeField] private Transform[] ways;

    private int index = -1;
    private bool canMove = false;
    private int movesNumber;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            index++;
            MoveOnce();
        }
        
    }

    public void MoveOnce()
    {
        movesNumber = GameManager.instance.GetDieNumber();
        Debug.Log(movesNumber);
        StartCoroutine(Moves());
    }

    IEnumerator Moves()
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

}
