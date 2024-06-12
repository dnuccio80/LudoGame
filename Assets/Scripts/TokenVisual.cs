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
        if (!tokenScript.GetCanPlay()) return; 
        GameManager.instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        spriteRenderer.sprite = tokenScript.GetPlayerSO().TokenSprite;
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.instance.GetCurrentPlayer() == tokenScript.GetColorPlayer()) 
        {
            spriteRenderer.sortingOrder = 20;
        } else
        {
            spriteRenderer.sortingOrder = 0;

        }
    }

    private void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(tokenScript.gameObject.transform.position.y * -1);

    }
}
