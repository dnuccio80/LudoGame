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
    [SerializeField] private AudioClip secureZoneSound;
    [SerializeField] private AudioClip startTurnSound;
    [SerializeField] private AudioClip winningSound;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void EmitBackToHouseSound()
    {
        audioSource.clip = backToHouseSound;
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
    public void EmitStartTurnSound()
    {
        audioSource.clip = startTurnSound;
        audioSource.Play();
    }

    public void EmitWinningSound()
    {
        audioSource.clip = winningSound;
        audioSource.Play();
    }



}
