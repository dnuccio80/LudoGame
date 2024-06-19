using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip backToHouseSound;
    [SerializeField] private AudioClip goalSound;
    [SerializeField] private AudioClip popSound;
    [SerializeField] private AudioClip rollDiceSound;
    [SerializeField] private AudioClip startGameSound;
    [SerializeField] private AudioClip secureZoneSound;
    [SerializeField] private AudioClip startTurnSound;
    [SerializeField] private AudioClip winningSound;
    [SerializeField] private AudioClip finishGameSound;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameManager.instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.instance.IsStartGameState())
        {
            EmitStartGameSound();
        }
    }

    public void EmitBackToHouseSound()
    {
        audioSource.clip = backToHouseSound;
        audioSource.Play();
    }
    public void EmitFinishGameSound()
    {
        audioSource.clip = finishGameSound;
        audioSource.Play();
    }

    

    public void EmitGoalSound()
    {
        audioSource.clip = goalSound;
        audioSource.Play();
    }
    
    public void EmitPopSound()
    {
        audioSource.clip = popSound;
        audioSource.Play();
    }

    public void EmitRollDiceSound()
    {
        audioSource.clip = rollDiceSound;
        audioSource.Play();
    }

    public void EmitSecureZoneSound()
    {
        audioSource.clip = secureZoneSound;
        audioSource.Play();
    }
    public void EmitStartGameSound()
    {
        AudioSource.PlayClipAtPoint(startGameSound, Vector3.zero);
    }
    public void EmitStartTurnSound()
    {
        audioSource.clip = startTurnSound;
        audioSource.Play();
    }

    public void EmitWinningSound()
    {
        AudioSource.PlayClipAtPoint(winningSound,Vector3.zero);
    }

    public void TurnOffSound()
    {
        audioSource.volume = 0f;
    }

    public void TurnOnSound()
    {
        audioSource.volume = 0.1f;
    }

}
