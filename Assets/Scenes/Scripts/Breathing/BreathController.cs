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
/*    public float startY = .87f;
    public float endY = .2f;*/

    public float duration = 5.0f;            // Duration of the color lerp in seconds

    public float _threshold = 0.4f;
    public float windowTime = 1f;               // The time window to ignore multiple triggers
    private float lastTriggerTime;              // The time of the last trigger
    private bool hasReachedThreshold = false;   // Flag to track if threshold has been reached
    private int counter = 0;                    // To count if it's breath in or out

    private Material skyboxMaterial;
    public bool _canBreath = false;



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
            if (Time.time - lastTriggerTime > windowTime)
            {
                if (_volume > _threshold && !hasReachedThreshold)
                {
                    counter += 1;
                    hasReachedThreshold = true;
                    // trigger coroutine
                    if (counter % 2 == 0)
                    {
                        StartCoroutine(SkyFadeRoutine(startColor, endColor));
                    }
                    else
                    {
                        StartCoroutine(SkyFadeRoutine(endColor, startColor));
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


    public IEnumerator SkyFadeRoutine(Color col1, Color col2)
    {
        // lerp alpha value
        float timer = 0;
        while (timer <= duration)
        {
            // Calculate the lerp value between 0 and 1
            float t = Mathf.Clamp01(timer / duration);

            /*float lerpedY = Mathf.Lerp(y1, y2, t);
            Vector4 lerped = new Vector4(0, lerpedY, 0, 1);
            skyboxMaterial.SetVector("_Direction", lerped);*/

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
