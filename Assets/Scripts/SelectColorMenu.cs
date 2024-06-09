using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectColorMenu : MonoBehaviour
{
    public event EventHandler OnBackButtonPressed;

    [SerializeField] private GameModeMenu gameModeMenu;
    [SerializeField] private Button backButton;
    [SerializeField] private Button playButton;

    [Header ("Toggle Buttons")]
    [SerializeField] private Button lightBlueToggle;
    [SerializeField] private Button redToggle;
    [SerializeField] private Button greenToggle;
    [SerializeField] private Button yellowToggle;

    private void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            OnBackButtonPressed?.Invoke(this, EventArgs.Empty);
            Hide();
        });

        playButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        gameModeMenu.OnNextButtonPressed += GameModeMenu_OnNextButtonPressed;
        Hide();
    }

    private void GameModeMenu_OnNextButtonPressed(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        playButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
