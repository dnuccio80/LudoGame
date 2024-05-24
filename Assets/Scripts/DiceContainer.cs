using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class DiceContainer : MonoBehaviour
{

    [SerializeField] private PlayerSO playerSO;

    public string GetPlayerColor()
    {
        return playerSO.ColorPlayer;
    }       

}
