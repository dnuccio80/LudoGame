using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceContainer : MonoBehaviour
{
    [SerializeField] private ColorPlayer colorPlayer;

    private enum ColorPlayer
    {
        Green,
        LightBlue,
        Red,
        Yellow
    }

    private void Start()
    {
        HandleTurns.instance.OnStateChanged += HandleTurns_OnStateChanged;
    }

    private void HandleTurns_OnStateChanged(object sender, HandleTurns.OnStateChangedEventArgs e)
    {
        
    }
}
