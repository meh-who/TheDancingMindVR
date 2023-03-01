using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ON.synth;
using OVR;

public class MusicController : MonoBehaviour
{
    [SerializeField] Plug_AudioSynth _synth;
    public OVRHand Hand { get; private set; } = null;

    private float frequency;
    private float ypos, preYpos, baseYpos;
    private bool isPinching, preIsPinching;

    // Start is called before the first frame update
    void Start()
    {
        Hand = GetComponent<OVRHand>();
        frequency = _synth.tones[1].frequency;
        preYpos = 0;
        isPinching = false;
        preIsPinching = false;
    }

    // Update is called once per frame
    void Update()
    {
        isPinching = Hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        if (isPinching != preIsPinching)
        {
            if (isPinching)
            {
                GetComponent<TrailRenderer>().enabled = true;
                baseYpos = gameObject.transform.position.y;
            }
            else
            {
                GetComponent<TrailRenderer>().enabled = false;
            }
            
        }
        if (isPinching)
        {
            ypos = gameObject.transform.position.y;
            float diff = (ypos - baseYpos) * 2000;
            if (diff > 1)
            {
                _synth.tones[1].frequency = 218;  

            }
            if (diff < -1)
            {
                _synth.tones[1].frequency = 206;
            }
            Debug.Log(diff);
        }

        preIsPinching = isPinching;
    }
}
