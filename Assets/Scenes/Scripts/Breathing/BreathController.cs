using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Normal.Realtime;

public class BreathController : MonoBehaviour
{
    public RealtimeAvatarVoice _voice;
    private float _volume;

    public Color startColor;
    public Color endColor;

    public Color startEmission;
    public Color endEmission;

    public float duration = 5.0f;            // Duration of the color lerp in seconds

    public float _threshold = 0.4f;
    public float windowTime = 1f;               // The time window to ignore multiple triggers
    private float lastTriggerTime;              // The time of the last trigger
    private bool hasReachedThreshold = false;   // Flag to track if threshold has been reached
    private int counter = 0;                    // To count if it's breath in or out

    private Material skyboxMaterial;
    private Material spheresMat;
    public bool _canBreath = false;

    public UnityEvent OnBreathIn;
    public UnityEvent OnBreathOut;


    void Start()
    {
        skyboxMaterial = RenderSettings.skybox;

    }

    void Update()
    {
        // volume 0-1
        _volume = _voice.voiceVolume;
        if (_canBreath == true)
        {
            // avoid multiple triggers within windowTime
            if (Time.time - lastTriggerTime > windowTime) 
            {
                if (_volume > _threshold && !hasReachedThreshold)
                {
                    counter += 1;
                    hasReachedThreshold = true;
                    // trigger coroutine
                    if (counter % 2 == 0)
                    {
                        OnBreathIn.Invoke();
                    }
                    else
                    {
                        OnBreathOut.Invoke();
                    }
                    lastTriggerTime = Time.time;
                }
                else if (_volume < _threshold)
                {
                    hasReachedThreshold = false;
                }
            }
        }
    }


    public void FadeInSphere()
    {
        if (!spheresMat)
        {
            spheresMat = GameObject.FindGameObjectWithTag("Breathable").GetComponent<Renderer>().material;
        }       
        StartCoroutine(SphereFadeRoutine(startEmission, endEmission));
    }
    public void FadeOutSphere()
    {
        if(!spheresMat)
        {
            spheresMat = GameObject.FindGameObjectWithTag("Breathable").GetComponent<Renderer>().material;
        }
        StartCoroutine(SphereFadeRoutine(endEmission, startEmission));
    }

    public void FadeInSky()
    {
        StartCoroutine(SkyFadeRoutine(startColor, endColor));
    }

    public void FadeOutSky()
    {
        StartCoroutine(SkyFadeRoutine(endColor, startColor));
    }

    public IEnumerator SphereFadeRoutine(Color col1, Color col2)
    {
        // lerp alpha value
        float timer = 0;
        while (timer <= duration)
        {
            // Calculate the lerp value between 0 and 1
            float t = Mathf.Clamp01(timer / duration);

            Color lerpedColor = Color.Lerp(col1, col2, t);
            spheresMat.SetColor("_EmissionColor", lerpedColor);
            spheresMat.EnableKeyword("_EMISSION");
            timer += Time.deltaTime;
            yield return null;
        }

        // make sure the final value is changed
        Color newColor2 = col2;
        spheresMat.SetColor("_EmissionColor", newColor2);
    }

    public IEnumerator SkyFadeRoutine(Color col1, Color col2)
    {
        // lerp alpha value
        float timer = 0;
        while (timer <= duration)
        {
            // Calculate the lerp value between 0 and 1
            float t = Mathf.Clamp01(timer / duration);

            Color lerpedColor = Color.Lerp(col1, col2, t);
            skyboxMaterial.SetColor("_MiddleColor", lerpedColor);

            timer += Time.deltaTime;
            yield return null;
        }

        // make sure the final value is changed
        Color newColor2 = col2;
        skyboxMaterial.SetColor("_MiddleColor", newColor2);
    }

}
