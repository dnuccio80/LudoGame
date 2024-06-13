using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameFinishedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] positionsTextArray;

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

        for (int i = 0; i < Mathf.Min(positionsTextArray.Length, winList.Count); i++)
        {
            if (winList[i] != null)
            {
                positionsTextArray[i].text = winList[i];
            }
            else
            {
                positionsTextArray[i].text = "";
            }
        }
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
