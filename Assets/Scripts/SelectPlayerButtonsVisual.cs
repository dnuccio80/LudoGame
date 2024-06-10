using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayerButtonsVisual : MonoBehaviour
{
    [SerializeField] private GameModeMenu gameModeMenu;
    [SerializeField] private GameObject twoPlayersCheckMark;
    [SerializeField] private GameObject fourPlayersCheckMark;

    private void Start()
    {
        gameModeMenu.OnTwoPlayersButtonPressed += GameModeMenu_OnTwoPlayersButtonPressed;
        gameModeMenu.OnFourPlayersButtonPressed += GameModeMenu_OnFourPlayersButtonPressed;

        twoPlayersCheckMark.SetActive(true);
        fourPlayersCheckMark.SetActive(false);
    }

    private void GameModeMenu_OnTwoPlayersButtonPressed(object sender, System.EventArgs e)
    {
        twoPlayersCheckMark.SetActive(true);
        fourPlayersCheckMark.SetActive(false);
    }

    private void GameModeMenu_OnFourPlayersButtonPressed(object sender, System.EventArgs e)
    {
        twoPlayersCheckMark.SetActive(false);
        fourPlayersCheckMark.SetActive(true);
    }
}
