using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class MicrophoneController : MonoBehaviour
{
    public RealtimeAvatarVoice _voice;

    AudioSource speaker;
    static int sampleSize = 512;
    float lowerT = 0.05f;
    float upperT = 0.5f;
    float[] data = new float[sampleSize];
    // Start is called before the first frame update
    public float intensity = 0;

    void Start()
    {
        /*speaker = gameObject.GetComponent<AudioSource>();
        speaker.clip = Microphone.Start(Microphone.devices[0].ToString(), true, 1, 44100);
        speaker.loop = true;
        while (!(Microphone.GetPosition(null) > 0))
        {
            speaker.Play();
        }*/
    }


    void Update()
    {
        // Get the voice volume
        float voiceVolume = _voice.voiceVolume;

        if (voiceVolume > .4)
        {

        }

        /*float sum = 0;
        foreach (float p in data)
        {
            sum += Mathf.Abs(voiceVolume);
        }
        float avg = sum / sampleSize * 100;
        avg = avg - lowerT;
        if (avg < 0) avg = 0;
        avg = avg / upperT;
        if (avg > 1) avg = 1;
        intensity = avg;*/
    }
}