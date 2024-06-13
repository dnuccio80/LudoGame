using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinCrown : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI numberWinText;

    private PlayerSO playerSO;
    
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

    public void SetPlayerSO(PlayerSO _playerSO)
    {
        playerSO = _playerSO;
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
