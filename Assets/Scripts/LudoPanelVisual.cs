using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LudoPanelVisual : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerSO[] playerSOArray;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateVisual(PlayerStats.GetPlayerColor());
    }

    private void UpdateVisual(string playerColor)
    {
        for(int i = 0; i < playerSOArray.Length; i++)
        {
            if(playerColor == playerSOArray[i].ColorPlayer)
            {
                spriteRenderer.sprite = playerSOArray[i].PanelSprite; 
                break;
            }
        }
    }


}
