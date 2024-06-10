using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectColorPlayerVisual : MonoBehaviour
{
    [SerializeField] private SelectColorMenu selectColorMenu;
    [SerializeField] private GameObject lightblueVisual;
    [SerializeField] private GameObject redVisual;
    [SerializeField] private GameObject greenVisual;
    [SerializeField] private GameObject yellowVisual;

    private void Start()
    {
        lightblueVisual.SetActive(true);
        redVisual.SetActive(false);
        greenVisual.SetActive(false);
        yellowVisual.SetActive(false);

        selectColorMenu.OnLightblueToggleButtonPressed += SelectColorMenu_OnLightblueToggleButtonPressed;
        selectColorMenu.OnRedToggleButtonPressed += SelectColorMenu_OnRedToggleButtonPressed;
        selectColorMenu.OnGreenToggleButtonPressed += SelectColorMenu_OnGreenToggleButtonPressed;
        selectColorMenu.OnYellowToggleButtonPressed += SelectColorMenu_OnYellowToggleButtonPressed;
    }

    private void SelectColorMenu_OnLightblueToggleButtonPressed(object sender, System.EventArgs e)
    {
        lightblueVisual.SetActive(true);
        redVisual.SetActive(false);
        greenVisual.SetActive(false);
        yellowVisual.SetActive(false);
    }
    private void SelectColorMenu_OnRedToggleButtonPressed(object sender, System.EventArgs e)
    {
        lightblueVisual.SetActive(false);
        redVisual.SetActive(true);
        greenVisual.SetActive(false);
        yellowVisual.SetActive(false);
    }

    private void SelectColorMenu_OnGreenToggleButtonPressed(object sender, System.EventArgs e)
    {
        lightblueVisual.SetActive(false);
        redVisual.SetActive(false);
        greenVisual.SetActive(true);
        yellowVisual.SetActive(false);
    }

    private void SelectColorMenu_OnYellowToggleButtonPressed(object sender, System.EventArgs e)
    {
        lightblueVisual.SetActive(false);
        redVisual.SetActive(false);
        greenVisual.SetActive(false);
        yellowVisual.SetActive(true);
    }

}
