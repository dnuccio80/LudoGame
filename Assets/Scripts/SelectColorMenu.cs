using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SelectColorMenu : MonoBehaviour
{
    public event EventHandler OnBackButtonPressed;
    public event EventHandler OnLightblueToggleButtonPressed;
    public event EventHandler OnRedToggleButtonPressed;
    public event EventHandler OnGreenToggleButtonPressed;
    public event EventHandler OnYellowToggleButtonPressed;

    [SerializeField] private GameModeMenu gameModeMenu;
    [SerializeField] private Button backButton;
    [SerializeField] private Button playButton;

    [Header ("Toggle Buttons")]
    [SerializeField] private Button lightBlueToggle;
    [SerializeField] private Button redToggle;
    [SerializeField] private Button greenToggle;
    [SerializeField] private Button yellowToggle;

    [Header("Player SO")]
    [SerializeField] private PlayerSO lightbluePlayerSO;
    [SerializeField] private PlayerSO redPlayerSO;
    [SerializeField] private PlayerSO greenPlayerSO;
    [SerializeField] private PlayerSO yellowPlayerSO;

    private void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            OnBackButtonPressed?.Invoke(this, EventArgs.Empty);
            Hide();
        });

        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.State.GameScene);
        });

        lightBlueToggle.onClick.AddListener(() =>
        {
            UpdateToggle(lightbluePlayerSO);
            OnLightblueToggleButtonPressed?.Invoke(this, EventArgs.Empty);
        });

        redToggle.onClick.AddListener(() =>
        {
            UpdateToggle(redPlayerSO);
            OnRedToggleButtonPressed?.Invoke(this, EventArgs.Empty);
        });

        greenToggle.onClick.AddListener(() =>
        {
            UpdateToggle(greenPlayerSO);
            OnGreenToggleButtonPressed?.Invoke(this, EventArgs.Empty);
        });

        yellowToggle.onClick.AddListener(() =>
        {
            UpdateToggle(yellowPlayerSO);
            OnYellowToggleButtonPressed?.Invoke(this, EventArgs.Empty);
        });
    }

    private void Start()
    {
        gameModeMenu.OnNextButtonPressed += GameModeMenu_OnNextButtonPressed;
        UpdateToggle(lightbluePlayerSO);
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

    private void UpdateToggle(PlayerSO playerSO)
    {
        PlayerStats.SetPlayerColor(playerSO);
    }
}
