using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalManager : MonoBehaviour
{
    public float startHandGrow = -.2f;
    public float endHandGrow = .2f;
    private GameObject[] handMeshes;
    [Space]
    public float startBodyGrow = -.5f;
    public float endBodyGrow = .5f;
    private Material bodyMat;
    [Space]
    public float duration = 4.0f;            // Duration of the color lerp in seconds

    private Material skyboxMaterial;
    public Color InitialSkyColor;

    private void Awake()
    {
        skyboxMaterial = RenderSettings.skybox;
        skyboxMaterial.SetColor("_MiddleColor", InitialSkyColor);
    }


    public void TriggerBreath()
    {
        BreathController bc = (BreathController)FindObjectOfType(typeof(BreathController));
        if (bc)
        {
            bc._canBreath = true;
            Debug.Log("hehe");
        }
        else
            Debug.Log("No breathe controller object could be found");
    }

    public void StopBreath()
    {
        BreathController bc = (BreathController)FindObjectOfType(typeof(BreathController));
        if (bc)
        {
            bc._canBreath = false;
            Debug.Log("nooo");
        }
        else
            Debug.Log("No breathe controller object could be found");
    }

    public void RevealHands()
    {

        handMeshes = GameObject.FindGameObjectsWithTag("HandMesh");
        foreach(GameObject mesh in handMeshes)
        {
            Material handMat = mesh.GetComponent<Renderer>().material;
            StartCoroutine(HandFadeRoutine(handMat, startHandGrow, endHandGrow));
            
        }
    }
    
    public IEnumerator HandFadeRoutine(Material mat, float grow1, float grow2)
    {
        // lerp alpha value
        float timer = 0;
        while (timer <= duration)
        {
            // Calculate the lerp value between 0 and 1
            float t = Mathf.Clamp01(timer / duration);

            float lerpedGlow = Mathf.Lerp(grow1, grow2, t);
            mat.SetFloat("_CutoffHeight", lerpedGlow);

            timer += Time.deltaTime;
            yield return null;
        }

        // make sure the final value is changed
        float newGlow = grow2;
        mat.SetFloat("_CutoffHeight", newGlow);
    }

    public void RevealBody()
    {
        bodyMat = GameObject.FindWithTag("Torso").GetComponent<Renderer>().material;
        if (bodyMat)
        {
            Debug.Log(bodyMat.name);
            StartCoroutine(BodyFadeRoutine());
        }

        else
            Debug.Log("No torso object could be found");
    }
    public IEnumerator BodyFadeRoutine()
    {
        // lerp alpha value
        float timer = 0;
        while (timer <= duration)
        {
            // Calculate the lerp value between 0 and 1
            float t = Mathf.Clamp01(timer / duration);

            float lerpedGrow = Mathf.Lerp(startBodyGrow, endBodyGrow, t);
            bodyMat.SetFloat("_Grow", lerpedGrow);

            timer += Time.deltaTime;
            yield return null;
        }
        // make sure the final value is changed
        float newGrow = endBodyGrow;
        bodyMat.SetFloat("_Grow", newGrow);
    }

}
