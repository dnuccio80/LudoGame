using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public event EventHandler OnPlayGameButonPressed;

    [SerializeField] private GameModeMenu gameModeMenu;
    [SerializeField] private Button playGameButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playGameButton.onClick.AddListener(() =>
        {
            OnPlayGameButonPressed?.Invoke(this, EventArgs.Empty);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    private void Start()
    {
        playGameButton.Select();
        gameModeMenu.OnBackButtonPressed += GameModeMenu_OnBackButtonPressed;
    }

    private void GameModeMenu_OnBackButtonPressed(object sender, EventArgs e)
    {
        playGameButton.Select();
    }
}
