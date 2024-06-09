using System;
using UnityEngine;
using UnityEngine.UI;

public class GameModeMenu : MonoBehaviour
{
    public event EventHandler OnNextButtonPressed;
    public event EventHandler OnBackButtonPressed;

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
}
