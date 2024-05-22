using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TokenMovement : MonoBehaviour
{

    [SerializeField] private Transform[] ways;

    int index = -1;
    bool canMove = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            index++;
            Move();
        }
        
    }

    private void Move()
    {
        float jumpPower = .5f;
        int numJumps = 1;
        float duration = .3f;

        transform.DOLocalJump(ways[index].transform.position, jumpPower, numJumps, duration);
    }

}
