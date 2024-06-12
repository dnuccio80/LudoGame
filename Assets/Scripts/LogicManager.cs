using UnityEngine;

public class LogicManager : MonoBehaviour
{

    [Header ("Token Script")]
    [SerializeField] private TokenScript[] playerTokenScriptsArray;
    [SerializeField] private TokenScript[] cpu1TokenScriptsArray;
    [SerializeField] private TokenScript[] cpu2TokenScriptsArray;
    [SerializeField] private TokenScript[] cpu3TokenScriptsArray;

    [Header ("Dice Containers")]
    [SerializeField] private DiceContainer playerDiceContainer;
    [SerializeField] private DiceContainer cpu1DiceContainer;
    [SerializeField] private DiceContainer cpu2DiceContainer;
    [SerializeField] private DiceContainer cpu3DiceContainer;

    [Header ("Cpu Brains")]
    [SerializeField] private CpuBehaviour cpu1CpuBehaviour;
    [SerializeField] private CpuBehaviour cpu2CpuBehaviour;
    [SerializeField] private CpuBehaviour cpu3CpuBehaviour;

    [Header("Goals")]
    [SerializeField] private Goal playerGoal;
    [SerializeField] private Goal cpu1Goal;
    [SerializeField] private Goal cpu2Goal;
    [SerializeField] private Goal cpu3Goal;

    [SerializeField] private PlayerSO[] playerSOArray;

    private int index;

    private void Awake()
    {
        SetVisual();
    }

    private void SetVisual()
    {

        foreach(TokenScript token in playerTokenScriptsArray)
        {
            token.SetPlayerSO(PlayerStats.GetPlayerSO());
            playerDiceContainer.SetPlayerSO(PlayerStats.GetPlayerSO());
            playerGoal.SetPlayerSO(PlayerStats.GetPlayerSO());
        }

        for (int i = 0; i < playerSOArray.Length; i++)
        {
            if (playerSOArray[i].ColorPlayer == PlayerStats.GetPlayerColor())
            {
                index = i;
                break;
            }
        }

        if (index < playerSOArray.Length - 1) index++;
        else index = 0;

        foreach (TokenScript token in cpu1TokenScriptsArray)
        {
            token.SetPlayerSO(playerSOArray[index]);
            
        }

        cpu1DiceContainer.SetPlayerSO(playerSOArray[index]);
        cpu1CpuBehaviour.SetPlayerSO(playerSOArray[index]);
        cpu1Goal.SetPlayerSO(playerSOArray[index]);


        if (index < playerSOArray.Length - 1) index++;
        else index = 0;

        foreach (TokenScript token in cpu2TokenScriptsArray)
        {
            token.SetPlayerSO(playerSOArray[index]);

        }

        cpu2DiceContainer.SetPlayerSO(playerSOArray[index]);
        cpu2CpuBehaviour.SetPlayerSO(playerSOArray[index]);
        cpu2Goal.SetPlayerSO(playerSOArray[index]);


        if (index < playerSOArray.Length - 1) index++;
        else index = 0;

        foreach (TokenScript token in cpu3TokenScriptsArray)
        {
            token.SetPlayerSO(playerSOArray[index]);

        }

        cpu3DiceContainer.SetPlayerSO(playerSOArray[index]);
        cpu3CpuBehaviour.SetPlayerSO(playerSOArray[index]);
        cpu3Goal.SetPlayerSO(playerSOArray[index]);

    }
}
