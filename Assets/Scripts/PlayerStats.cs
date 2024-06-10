using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public static class PlayerStats
{
    private static int numPlayers = 2;
    private static string playerColor;

    public static void SetNumberPlayers(int _numPlayers)
    {
        numPlayers = _numPlayers;
    }

    public static void SetPlayerColor(PlayerSO playerSO)
    {
        playerColor = playerSO.ColorPlayer;
    }

    public static int GetNumberPlayers() { return numPlayers; }

    public static string GetPlayerColor() {  return playerColor; }


}
