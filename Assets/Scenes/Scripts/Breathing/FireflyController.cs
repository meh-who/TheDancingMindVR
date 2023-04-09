using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyController : MonoBehaviour
{
    public GameObject breath;
    private bool on = false;
    private float intensity = 0f;
    private ParticleSystem fireflies;
    void Start()
    {
        fireflies = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        intensity = breath.GetComponent<MicrophoneController>().intensity;



        if (intensity > 0)
        {
            fireflies.Emit(100);
            //on = true;
            //fireflies.Play();
        }
        if (on && intensity == 0)
        {
            //on = false;
        }

    }
}