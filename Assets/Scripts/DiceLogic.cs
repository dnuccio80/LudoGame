using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class DiceLogic : MonoBehaviour
{

    private DiceContainer diceContainer;
    [SerializeField] private Sprite[] diceNumbersSpriteArray;


    private bool canRollDice;

    private void Awake()
    {
        diceContainer = GetComponentInParent<DiceContainer>();
    }

    private void Start()
    {
        GameManager.instance.OnRollDiceGameState += GameManager_OnRollDiceGameState;
    }

    private void GameManager_OnRollDiceGameState(object sender, System.EventArgs e)
    {
        canRollDice = diceContainer.GetPlayerColor() == GameManager.instance.GetCurrentPlayer();

        if(canRollDice)
        {
            Show();
        } else
        {
            Hide();
        }
    }

    public void TryRollDice()
    {
        if (!canRollDice) return;

        int minDieNumber = 1;
        int maxDieNumber = 7;

        int dieNumber = Random.Range(minDieNumber, maxDieNumber);
        GetComponent<SpriteRenderer>().sprite = diceNumbersSpriteArray[dieNumber - 1];
        Debug.Log(dieNumber);

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
