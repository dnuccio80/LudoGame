using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksParticleSystem : MonoBehaviour
{
    private ParticleSystem fireworksPS;

    [SerializeField] private AudioClip fireworksBorn;
    [SerializeField] private AudioClip fireworksDeath;

    private int currentNumberParticles = 0;


    private void Awake()
    {
        fireworksPS = GetComponent<ParticleSystem>();
    }

    private void Update()
    {

        if(fireworksPS.particleCount < currentNumberParticles) EmitSound(fireworksDeath);

        if(fireworksPS.particleCount > currentNumberParticles) EmitSound(fireworksBorn);

        currentNumberParticles = fireworksPS.particleCount;
    }

    private void EmitSound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Vector3.zero);
    }     


}
