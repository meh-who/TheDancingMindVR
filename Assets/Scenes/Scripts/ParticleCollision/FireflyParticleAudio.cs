using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyParticleAudio : MonoBehaviour
{
    AudioSource audioSource;
    public float lifetime = 1f;

    public float pitchRandomizer = 1.0f;
    public float volumeRandomizer = .25f;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.pitch += Random.Range(-pitchRandomizer / 2f, pitchRandomizer / 2f);
        audioSource.volume += Random.Range(-volumeRandomizer / 2f, volumeRandomizer / 2f);

        audioSource.Play();

        Destroy(gameObject, lifetime);
    }

}
