using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUi : MonoBehaviour
{

    private const string ON_TEXT = "ON";
    private const string OFF_TEXT = "OFF";

    [SerializeField] private Button crossButton;
    [SerializeField] private Button sfxButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitGameButton;

    [SerializeField] private SettingsButtonContainer settingsButtonContainer;

    [Header("Sfx Button Visuals")]
    [SerializeField] private Image sfxButtonImage;
    [SerializeField] private Image circleImage;
    [SerializeField] private TextMeshProUGUI sfxButtonText;
    [SerializeField] private Color sfxButtonOnColor;
    [SerializeField] private Color sfxButtonOffColor;

    private bool soundOn = true;

    private void Awake()
    {
        crossButton.onClick.AddListener(() =>
        {
            Hide();
        });

        sfxButton.onClick.AddListener(() =>
        {
            ToggleSfxButton();
            UpdateVisualVfxButton();
        });

        resumeButton.onClick.AddListener(() =>
        {
            Hide();
        });

        quitGameButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.State.MainMenuScene);
        });
    }

    private void Start()
    {
        settingsButtonContainer.OnOpenSettingsButtonPressed += SettingsButtonContainer_OnOpenSettingsButtonPressed;
        UpdateVisualVfxButton();
        Hide();
    }

    private void SettingsButtonContainer_OnOpenSettingsButtonPressed(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ToggleSfxButton()
    {
        soundOn = !soundOn;

        if (soundOn) SoundManager.Instance.TurnOnSound();
        else SoundManager.Instance.TurnOffSound();
    }

    private void UpdateVisualVfxButton()
    {
        if (soundOn)
        {
            sfxButtonText.text = ON_TEXT;
            sfxButtonImage.color = sfxButtonOnColor;
            circleImage.color = sfxButtonOnColor;

        }
        else
        {
            sfxButtonText.text = OFF_TEXT;
            sfxButtonImage.color = sfxButtonOffColor;
            circleImage.color = sfxButtonOffColor;

        }
    }

}
