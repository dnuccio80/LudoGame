using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinCrown : MonoBehaviour
{

    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private TextMeshProUGUI numberWinText;

    private void Start()
    {
        GameManager.instance.OnPlayerWin += GameManager_OnPlayerWin;
        Hide();
    }

    private void GameManager_OnPlayerWin(object sender, GameManager.OnPlayerWinEventArgs e)
    {
        if(e.playerSO.ColorPlayer == playerSO.ColorPlayer)
        {
            Show();
            numberWinText.text = e.playerWinPosition.ToString();
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
