using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;


public class MPHand : RealtimeComponent<HandDataModel>
{
    public Transform[] _FingerRots;
    [SerializeField]
    private bool LeftOrRight;
    
    public Vector3 _poss;

    public void StoreHandData(Vector3 position, Quaternion rotation)
    {
        // Only update if its changed by the local client
        if (!realtimeView.isOwnedLocallySelf)
            return;
        // store the hand data
        model.handPosition = position;
        model.handRotation = rotation;
    }
    
    public void CreateFingerModelRotations()
    {
        // Only update if its changed by the local client
        if (!realtimeView.isOwnedLocallySelf)
            return;
        for (int i = 0; i < _FingerRots.Length; i++)
        {
            FingerRotModel fRot = new FingerRotModel();
            fRot.rotation = _FingerRots[i].localRotation;
            model.fingerRotations.Add(fRot);
        }
    }
    public void StoreFingerRotations(int index, Quaternion rotation)
    {
        // Only update if its changed by the local client
        if (!realtimeView.isOwnedLocallySelf)
            return;
        // store finger rotation data
        model.fingerRotations[index].rotation = rotation;
    }


    void Update()
    {
        
        // apply the hand data model
        transform.position = model.handPosition;
        _poss = model.handPosition; //for debug
        transform.localRotation = model.handRotation;

        // apply finger rotations model
        for (int i = 0; i < _FingerRots.Length; i++)
        {
            _FingerRots[i].localRotation = model.fingerRotations[i].rotation;
        }

    }
}
