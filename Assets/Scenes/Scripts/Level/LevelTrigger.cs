using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelTrigger : MonoBehaviour
{
    public string handL;
    public string handR;
    public UnityEvent onHandsTouching;
    private bool enteredL = false;
    private bool enteredR = false;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name == handL)
        {
            enteredL = true;
            Debug.Log("L");
        }
        if (other.name == handR)
        {
            enteredR = true;
            Debug.Log("R");
        }
        if (enteredL && enteredR)
        {
            onHandsTouching.Invoke();
            Debug.Log("LR");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.name == handL)
        {
            enteredL = false;
        }

        if (other.name == handR)
        {
            enteredR = false;
        }
    }

}
