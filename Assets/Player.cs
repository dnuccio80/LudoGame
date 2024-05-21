using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{

    [SerializeField] private Transform[] ways;

    private Animator animator;

    int index = -1;
    bool canMove = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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

        animator.SetTrigger("Move");

        transform.DOMove(ways[index].transform.position, .3f)
            .SetEase(Ease.Linear);

    }

}
