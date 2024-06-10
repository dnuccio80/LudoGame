using System;
using UnityEngine;
using UnityEngine.UI;

public class GameModeMenu : MonoBehaviour
{
    public event EventHandler OnNextButtonPressed;
    public event EventHandler OnBackButtonPressed;
    public event EventHandler OnTwoPlayersButtonPressed;
    public event EventHandler OnFourPlayersButtonPressed;

    [SerializeField] private MainMenuButtons mainMenuButtons;
    [SerializeField] private SelectColorMenu selectColorMenu;
    [SerializeField] private Button twoPlayersToggleButton;
    [SerializeField] private Button fourPlayersToggleButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;


    private void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            OnBackButtonPressed?.Invoke(this, EventArgs.Empty);
            Hide();
        });

        nextButton.onClick.AddListener(() =>
        {
            OnNextButtonPressed?.Invoke(this,EventArgs.Empty);
            Hide();
        });

        twoPlayersToggleButton.onClick.AddListener(() =>
        {
            UpdateToggle(2);
            OnTwoPlayersButtonPressed?.Invoke(this, EventArgs.Empty);
        });

        fourPlayersToggleButton.onClick.AddListener(() =>
        {
            UpdateToggle(4);
            OnFourPlayersButtonPressed?.Invoke(this, EventArgs.Empty);
        });
    }

    private void Start()
    {
        mainMenuButtons.OnPlayGameButonPressed += MainMenuButtons_OnPlayGameButonPressed;
        selectColorMenu.OnBackButtonPressed += SelectColorMenu_OnBackButtonPressed;
        Hide();
    }

    private void SelectColorMenu_OnBackButtonPressed(object sender, EventArgs e)
    {
        Show();
    }

    private void MainMenuButtons_OnPlayGameButonPressed(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        nextButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void UpdateToggle(int _numPlayers)
    {
        PlayerStats.SetNumberPlayers(_numPlayers);
    }
}
