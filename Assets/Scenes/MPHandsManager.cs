using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using OculusSampleFramework;

public class MPHandsManager : MonoBehaviour
{
    [SerializeField] private Realtime _realtime;
    [SerializeField] private GameObject _handPrefabL;
    [SerializeField] private GameObject _handPrefabR;
    [SerializeField] private Transform _anchorL;
    [SerializeField] private Transform _anchorR;
    private bool _hasHands = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_realtime.connected)
            return;
        if (!_hasHands)
        {
            GameObject handObjL = Realtime.Instantiate(_handPrefabL.name, ownedByClient: true, useInstance: _realtime);
            handObjL.transform.SetParent(_anchorL);
            handObjL.transform.localPosition = Vector3.zero;
            handObjL.transform.localRotation = new Quaternion(0, 0, 0, 0);
            GameObject handObjR = Realtime.Instantiate(_handPrefabR.name, ownedByClient: true, useInstance: _realtime);
            handObjR.transform.SetParent(_anchorR);
            handObjR.transform.localPosition = Vector3.zero;
            handObjR.transform.localRotation = new Quaternion(0, 0, 0, 0);
            _hasHands = true;
        }
    }
}
