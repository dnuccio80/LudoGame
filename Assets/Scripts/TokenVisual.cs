using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenVisual : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private TokenScript tokenScript;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tokenScript = GetComponentInParent<TokenScript>();
    }

    private void Start()
    {
        GameManager.instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.instance.GetCurrentPlayer() == tokenScript.GetColorPlayer()) 
        {
            spriteRenderer.sortingOrder = 1;
        } else
        {
            spriteRenderer.sortingOrder = 0;

        }
    }
}
