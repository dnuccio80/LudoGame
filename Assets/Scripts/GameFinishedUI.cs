using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameFinishedUI : MonoBehaviour
{
    private const string CONGRATULATIONS_TEXT = "Congratulations!";
    private const string YOU_LOSE_TEXT = "You Lose!";

    [SerializeField] private TextMeshProUGUI[] positionsTextArray;
    [SerializeField] private TextMeshProUGUI congratsText;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.State.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.instance.OnGameFinished += GameManager_OnGameFinished;
        Hide();
    }

    private void GameManager_OnGameFinished(object sender, GameManager.OnGameFinishedEventArgs e)
    {
        Show();
        UpdateVisual(e.winList);
    }

    private void UpdateVisual(List<string> winList)
    {
        CleanPositions();
        UpdateCongratsText(GameManager.instance.GetPlayerFinishPosition(), PlayerStats.GetNumberPlayers());
        for (int i = 0; i < winList.Count; i++) positionsTextArray[i].text = winList[i];
    }

    private void CleanPositions()
    {
        foreach (TextMeshProUGUI text in positionsTextArray) text.text = "";
    }

    private void UpdateCongratsText(int playerPosition, int playersQuant)
    {
       switch(playersQuant)
        {
            case 2:
                if(playerPosition == 1) congratsText.text = CONGRATULATIONS_TEXT;
                else congratsText.text = YOU_LOSE_TEXT;
                break;
            case 4:
                if (playerPosition < 3) congratsText.text = CONGRATULATIONS_TEXT;
                else congratsText.text = YOU_LOSE_TEXT;
                break;
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
        mainMenuButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
