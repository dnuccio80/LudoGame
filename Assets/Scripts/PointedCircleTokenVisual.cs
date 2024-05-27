using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PointedCircleTokenVisual : MonoBehaviour
{
    [SerializeField] private TokenScript tokenScript;

    private void Start()
    {
        tokenScript.OnStateChanged += TokenScript_OnStateChanged;
    }

    private void TokenScript_OnStateChanged(object sender, System.EventArgs e)
    {
        (tokenScript.canMoveToken() ? (Action)Show : Hide)();
    }

    private void Show()
    {
        gameObject.SetActive(true);

    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
