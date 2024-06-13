using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameFionishedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI firstPositionText;
    [SerializeField] private TextMeshProUGUI secondPositionText;
    [SerializeField] private TextMeshProUGUI thirdPositionText;
    [SerializeField] private TextMeshProUGUI fourthPositionText;



    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
