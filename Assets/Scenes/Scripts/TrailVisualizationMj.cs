using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class TrailVisualizationMj : MonoBehaviour
{
    public bool _canTrail = false;
    private mj.RealtimeAvatarManager _realtimeAvatarManager;
    private Realtime _realtime;
    // Start is called before the first frame update
    private void Awake()
    {
        _realtimeAvatarManager = GetComponent<mj.RealtimeAvatarManager>();
        
    }
    void Start()
    {
        

    }

    public void EnableLeftTrail()
    {
        if (_realtimeAvatarManager.localAvatar == null)
            return;
        TrailRenderer trL = _realtimeAvatarManager.localAvatar.GetComponentsInChildren<TrailRenderer>()[1];
        if (trL)
        {
            trL.enabled = true;
        }        
    }

    public void DisableLeftTrail()
    {
        if (_realtimeAvatarManager.localAvatar == null)
            return;
        TrailRenderer trL = _realtimeAvatarManager.localAvatar.GetComponentsInChildren<TrailRenderer>()[1];
        if (trL)
        {
            trL.enabled = false;
        }
    }


    public void EnableRightTrail()
    {
        if (_realtimeAvatarManager.localAvatar == null)
            return;
        TrailRenderer trL = _realtimeAvatarManager.localAvatar.GetComponentsInChildren<TrailRenderer>()[0];
        if (trL)
        {
            trL.enabled = true;
        }
    }

    public void DisableRightTrail()
    {
        if (_realtimeAvatarManager.localAvatar == null)
            return;
        TrailRenderer trL = _realtimeAvatarManager.localAvatar.GetComponentsInChildren<TrailRenderer>()[0];
        if (trL)
        {
            trL.enabled = false;
        }
    }

}
