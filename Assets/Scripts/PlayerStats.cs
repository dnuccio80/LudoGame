using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public static class PlayerStats
{
    private static int numPlayers = 2;
    private static PlayerSO playerSO;

    public static void SetNumberPlayers(int _numPlayers)
    {
        numPlayers = _numPlayers;
    }

    public static void SetPlayerSO(PlayerSO _playerSO)
    {
        playerSO = _playerSO;
    }

    public static int GetNumberPlayers() { return numPlayers; }

    public static string GetPlayerColor() {  return playerSO.ColorPlayer; }

    public static PlayerSO GetPlayerSO() { return playerSO; }
    public static Sprite GetTokenSprite()
    {
        return playerSO.TokenSprite;
    }

}
