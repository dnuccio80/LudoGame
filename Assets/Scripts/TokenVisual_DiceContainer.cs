using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenVisual_DiceContainer : MonoBehaviour
{
    private DiceContainer diceContainer;
    private SpriteRenderer tokenSprite;

    private void Awake()
    {
        tokenSprite = GetComponent<SpriteRenderer>();
        diceContainer = GetComponentInParent<DiceContainer>();
    }

    void Start()
    {
        if (!diceContainer.GetCanPlay()) return;
        tokenSprite.sprite = diceContainer.GetPlayerSO().TokenSprite;
    }

    
}
