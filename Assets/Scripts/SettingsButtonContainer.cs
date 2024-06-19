using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonContainer : MonoBehaviour
{
    [SerializeField] private Button openSettingsButton;
    
    public event EventHandler OnOpenSettingsButtonPressed;

    private void Awake()
    {
        openSettingsButton.onClick.AddListener(() =>
        {
            OnOpenSettingsButtonPressed?.Invoke(this, EventArgs.Empty);
        });
    }

}
