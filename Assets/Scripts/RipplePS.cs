using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RipplePS : MonoBehaviour
{

    [SerializeField] private TokenScript tokenScript;
    [SerializeField] private ParticleSystem ripplePS;


    private void Start()
    {
        var mainModule = ripplePS.main;
        mainModule.startColor = tokenScript.GetPlayerSO().RippleColor;
    }
}

