using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailVisualization : MonoBehaviour
{
    public bool _canTrail = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableTrail()
    {

        TrailRenderer tr = (TrailRenderer)FindObjectOfType(typeof(TrailRenderer));
        if (tr)
        {
            tr.enabled = true;
        }
        
    }

    public void DisableTrail()
    {
        TrailRenderer tr = (TrailRenderer)FindObjectOfType(typeof(TrailRenderer));
        if (tr)
        {
            tr.enabled = false;
        }
        
    }
}
